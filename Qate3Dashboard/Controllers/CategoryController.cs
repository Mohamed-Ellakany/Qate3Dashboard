using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.Helpers;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index(int? DepartmentId)
        {
            if (DepartmentId == null || DepartmentId == 0)
            {
                return RedirectToAction("Index", "Department");
            }
            else
            {

            var categories = (await _unitOfWork.Repository<Category>().GetAllAsync())
                                .Where(C => C.Dept_Id == DepartmentId);

            var dept = await _unitOfWork.Repository<Department>().GetByIdAsync(DepartmentId);

            ViewBag.DepartmentName = dept.Dept_Title;

                ViewBag.DeptId = DepartmentId;
             var MappedCategory = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoriesViewModel>>(categories);

            return View(MappedCategory);
           
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateCategory(string? errorMessage , int? DepartmentId)
        {
            ViewBag.error = errorMessage;

            ViewBag.DeptId=DepartmentId;

            ViewBag.departments =await _unitOfWork.Repository<Department>().GetAllAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoriesViewModel categoryVM  ,int? DepartmentId)
        {
            ViewBag.departments =await _unitOfWork.Repository<Department>().GetAllAsync();
            if (ModelState.IsValid)
            {
                ViewBag.DeptId = DepartmentId;
                var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

                foreach (var cat in categories)
                {
                    if (categoryVM.Cat_Title == cat.Cat_Title)
                    {


                        return RedirectToAction("CreateCategory", routeValues: new { errorMessage = "this category is already exist" });
                    }

                }

                categoryVM.Cat_ImageName = DocumentSettings.UploadFile(categoryVM.Image, "Departments\\Categories");

                var category = _mapper.Map<CategoriesViewModel, Category>(categoryVM);

                await _unitOfWork.Repository<Category>().AddAsync(category);

                int count = _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index), routeValues: new { DepartmentId = categoryVM.Dept_Id });

            }
            
                return View(categoryVM);
            
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string? errorMessage , int DepartmentId)
        {


            if (!id.HasValue)
                return BadRequest();

            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id.Value);

            if (category is null)
                return NotFound();

            ViewBag.error = errorMessage;

            ViewBag.DeptId = DepartmentId;

            ViewBag.departments =await _unitOfWork.Repository<Department>().GetAllAsync();
            //var dept = await _unitOfWork.Repository<Department>().GetByIdAsync(DepartmentId);

            //ViewBag.DepartmentName = dept.Dept_Title;


            var categoryVM = _mapper.Map<Category, CategoriesViewModel>(category);

            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriesViewModel categoryVM, [FromRoute] int? id)
        {


            if (id != categoryVM.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(categoryVM);


            //var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

            //foreach (var cat in categories)
            //{
            //    if (categoryVM.Cat_Title == cat.Cat_Title)
            //    {


            //        return RedirectToAction("CreateCategory", routeValues: new { errorMessage = "this category is already exist" });
            //    }

            //}



            var OldCategory = await _unitOfWork.Repository<Category>().GetByIdAsync(id.Value);

            categoryVM.Cat_ImageName = DocumentSettings.UploadFile(categoryVM.Image, "Departments\\Categories");

            var category = _mapper.Map<CategoriesViewModel, Category>(categoryVM);

            _unitOfWork.Repository<Category>().Detach(OldCategory);

            _unitOfWork.Repository<Category>().Update(category);

            int count = _unitOfWork.Complete();

            if (count > 0)
            {
                DocumentSettings.DeleteFile(OldCategory.Cat_ImageName, "Departments\\Categories");

            }

            return RedirectToAction(nameof(Index) , routeValues : new { DepartmentId = categoryVM.Dept_Id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

            if (category is null)
                return NotFound();
            var categoryVM = _mapper.Map<Category, CategoriesViewModel>(category);

            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoriesViewModel categoryVM, [FromRoute] int? id)
        {

            if (id != categoryVM.Id) return BadRequest();



            var category = _mapper.Map<CategoriesViewModel, Category>(categoryVM);

            await _unitOfWork.Repository<Category>().DeleteAsync(category);
            int count = _unitOfWork.Complete();

            if (count > 0 && categoryVM.Cat_ImageName is not null)
            {
            DocumentSettings.DeleteFile(categoryVM.Cat_ImageName, "Departments\\Categories");

            var Products =await _unitOfWork.Repository<Product>().GetAllAsync();
            
                foreach(var product in Products)
                {
                    if(product.categoryId == categoryVM.Id)
                    {
                        DocumentSettings.DeleteFile(product.Prod_ImageName, "Departments\\Categories\\Products");

                    }
                }

            }
               
            return RedirectToAction(nameof(Index), routeValues: new { DepartmentId = categoryVM.Dept_Id });
        }

    }


}


