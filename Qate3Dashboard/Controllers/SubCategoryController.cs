using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int CategoryId)
        {
            ViewBag.CategoryId = CategoryId;

            var SubCategories = await _unitOfWork.Repository<SubCategory>().GetAllAsync();

            var MappedSubCategories = _mapper.Map<IEnumerable<SubCategory>, IEnumerable<SubCategoryViewModel>>(SubCategories);
         
            return View(MappedSubCategories);
            
        }
    }
}
