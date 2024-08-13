using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;
using Qate3Dashboard.Errors;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryApiController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{DepartmentId}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesInDepartment(int? DepartmentId)
        {

            if (DepartmentId == null || DepartmentId == 0)
            {
                return BadRequest(new ApiResponse(400 ));
            }
            else
            {
                var department =await _unitOfWork.Repository<Department>().GetByIdAsync(DepartmentId);
                if (department == null)
                {
                    return NotFound(new ApiResponse(404 , "no department with this id"));
                }


                var categories = (await _unitOfWork.Repository<Category>().GetAllAsync())
                                    .Where(C => C.Dept_Id == DepartmentId);

               
               
                var MappedCategories = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

                return Ok(MappedCategories);

            }
        
        
        }


    }
}
