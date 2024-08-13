using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qate3Dashboard.DTOs;
using Qate3Dashboard.Errors;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsApiController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentsApiController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(IEnumerable<DepartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {

            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            if (!departments.Any())
            {
                return BadRequest(new ApiResponse(404, "that's no Departments "));
            }

            var MappedDepartments = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departments);


            return Ok(MappedDepartments);


        }

        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _unitOfWork.Repository<Department>().GetByIdAsync(id);
            if (department is null)
            {
                return BadRequest(new ApiResponse(404, "that's no department with this id"));
            }

            var MappedDepartment = _mapper.Map<Department, DepartmentDto>(department);

            return Ok(MappedDepartment);

        }

    }
}
