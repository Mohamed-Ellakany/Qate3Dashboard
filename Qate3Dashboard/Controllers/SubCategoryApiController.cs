using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;

namespace Qate3Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryApiController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategories()
        {
            var SubCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsync();

            var MappedSubCategories = _mapper.Map<IEnumerable<SubCategory>, IEnumerable<SubCategoryDto>>(SubCategories);

            return Ok(MappedSubCategories);
        }


    }
}
