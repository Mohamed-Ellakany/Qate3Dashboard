using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.Helpers;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
            var MappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDepartment);
        }


        [HttpGet]
        public async Task<IActionResult> CreateDepartment(string? errorMessage)
        {
            ViewBag.error = errorMessage;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {

                var departments = await _unitOfWork.Repository<Department>().GetAllAsync();

                foreach (var dept in departments)
                {
                    if (departmentVM.Dept_Title == dept.Dept_Title)
                    {


                        return RedirectToAction("CreateDepartment", routeValues: new { errorMessage = "this Department is already exist" });
                    }

                }



                departmentVM.Dept_ImageName = DocumentSettings.UploadFile(departmentVM.Image, "Departments");

               var Dept= _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                 await _unitOfWork.Repository<Department>().AddAsync(Dept);
                int count = _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));

            }

            return View(departmentVM);

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id , string? errorMessage)
        { 
            ViewBag.error = errorMessage;
        
            if (!id.HasValue)
                return BadRequest();

            var department =await _unitOfWork.Repository<Department>().GetByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            

            var departmentVM =_mapper.Map<Department, DepartmentViewModel>(department);
            return View(departmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( DepartmentViewModel departmentVM , [FromRoute] int? id)
        {


            if (id != departmentVM.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);

            //var departments = await _unitOfWork.Repository<Department>().GetAllAsync();

            //foreach (var dept in departments)
            //{
            //    if (departmentVM.Dept_Title == dept.Dept_Title)
            //    {


            //        return RedirectToAction("CreateDepartment", routeValues: new { id = id ,errorMessage = "this Department is already exist" });
            //    }

            //}



            var OldDepartment = await _unitOfWork.Repository<Department>().GetByIdAsync(id.Value);

            departmentVM.Dept_ImageName = DocumentSettings.UploadFile(departmentVM.Image, "Departments");

            var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

            _unitOfWork.Repository<Department>().Detach(OldDepartment);

            _unitOfWork.Repository<Department>().Update(department);

             int count = _unitOfWork.Complete();

            if (count > 0)
            {
                DocumentSettings.DeleteFile(OldDepartment.Dept_ImageName, "Departments");
               
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department =await _unitOfWork.Repository<Department>().GetByIdAsync(id);
           
            if (department is null)
                return NotFound();
             var departmentVM=_mapper.Map<Department, DepartmentViewModel>(department);

            return View(departmentVM);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( DepartmentViewModel departmentVM, [FromRoute] int? id)
        {


            if (id == null || id != departmentVM.Id) return BadRequest();
            

            var department = await _unitOfWork.Repository<Department>().GetByIdAsync(departmentVM.Id);


            if (department == null)  return NotFound();
            

            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

            var departmentCategories = categories.Where(c => c.Dept_Id == departmentVM.Id);

            var Products = await _unitOfWork.Repository<Product>().GetAllAsync();


            // Delete category images
            foreach (var category in departmentCategories)
            {
                DocumentSettings.DeleteFile(category.Cat_ImageName, "Departments\\Categories");

                foreach(var  product in Products)
                {
                    if(product.categoryId ==  category.Id)  
                    DocumentSettings.DeleteFile(product.Prod_ImageName, "Departments\\Categories\\Products");
                }
            }
         

            // Delete the department entity
           await _unitOfWork.Repository<Department>().DeleteAsync(department);

            int count =  _unitOfWork.Complete();


            if (count > 0 && departmentVM.Dept_ImageName is not null)
            {
                DocumentSettings.DeleteFile(departmentVM.Dept_ImageName, "Departments");


            }
                


            return RedirectToAction(nameof(Index));
        }

    }


}
    

