using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.Helpers;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(int SubCategoryId, int CategoryId )
        {
            

            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(CategoryId);

            ViewBag.categoryName = category.Cat_Title;

            var Products = _unitOfWork.Repository<Product>().GetAllAsync().Result.Where(p=>p.SubcategoryId==SubCategoryId && p.categoryId ==CategoryId);
            ViewBag.CatIdNow = CategoryId;
            ViewBag.SubCatNow = SubCategoryId;
            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(Products);

            return View(mappedProducts);

        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct(string? errorMessage , int? CatId ,int? SubCatId )
        {
            // ViewBag.Products =await _unitOfWork.Repository<Product>().GetAllAsync();
            ViewBag.error = errorMessage;

            ViewBag.categories =await _unitOfWork.Repository<Category>().GetAllAsync();

            ViewBag.Subcategories =await _unitOfWork.Repository<SubCategory>().GetAllAsync();

            ViewBag.CatId = CatId;

            ViewBag.SubCatId = SubCatId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            //ViewBag.Subcategories = _unitOfWork.Repository<SubCategory>().GetAllAsync().Result;


            if(ModelState.IsValid)
            {
                var Products =await _unitOfWork.Repository<Product>().GetAllAsync();

                foreach(var prod in Products)
                {
                    if(productVM.Prod_Title == prod.Prod_Title)
                    {
                       

                      return RedirectToAction("CreateProduct" ,routeValues: new { errorMessage = "this product is already exist" });
                    }

                }
                
                productVM.Prod_ImageName = DocumentSettings.UploadFile(productVM.Image, "Departments\\Categories\\Products");

                var product = _mapper.Map<ProductViewModel, Product>(productVM);

                await _unitOfWork.Repository<Product>().AddAsync(product);

                int count = _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index), routeValues: new { SubCategoryId = productVM.SubcategoryId, CategoryId = productVM.categoryId });

            }
            //DocumentSettings.DeleteFile(productVM.Prod_ImageName, "Departments\\Categories\\Products");

            return View(productVM);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id , string errorMessage , int CatId , int SubCatId)
        {
            if (!id.HasValue) return BadRequest();

            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id.Value);

            if (product is null) return NotFound();

            ViewBag.error = errorMessage;

            var MappedProduct = _mapper.Map<Product, ProductViewModel>(product);

            ViewBag.categories =await _unitOfWork.Repository<Category>().GetAllAsync();

            ViewBag.Subcategories =await _unitOfWork.Repository<SubCategory>().GetAllAsync();

            ViewBag.CatId = CatId;

            ViewBag.SubCatId = SubCatId;
            return View(MappedProduct);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel productVM, [FromRoute] int? id)
        {


            if (id != productVM.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(productVM);

            //var Products = await _unitOfWork.Repository<Product>().GetAllAsync();

            //foreach (var prod in Products)
            //{
            //    if (productVM.Prod_Title == prod.Prod_Title)
            //    {


            //        return RedirectToAction("Edit", routeValues: new { errorMessage = "this product is already exist" });
            //    }

            //}

            var OldProduct = await _unitOfWork.Repository<Product>().GetByIdAsync(id.Value);

            productVM.Prod_ImageName = DocumentSettings.UploadFile(productVM.Image, "Departments\\Categories\\Products");

            var product = _mapper.Map<ProductViewModel, Product>(productVM);

            _unitOfWork.Repository<Product>().Detach(OldProduct);

            _unitOfWork.Repository<Product>().Update(product);

            int count = _unitOfWork.Complete();

            if (count > 0)
            {
                DocumentSettings.DeleteFile(OldProduct.Prod_ImageName, "Departments\\Categories\\Products");

            }

            return RedirectToAction(nameof(Index), routeValues: new { SubCategoryId = productVM.SubcategoryId, CategoryId = productVM.categoryId });
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)  return BadRequest();

            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product is null) return NotFound();

            var productVM = _mapper.Map<Product, ProductViewModel>(product);

            return View(productVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductViewModel productVM, [FromRoute] int? id)
        {

            if (id != productVM.Id) return BadRequest();

            var prod =await _unitOfWork.Repository<Product>().GetByIdAsync(id.Value);

            var catId = prod.categoryId;
            var SubCatId = prod.SubcategoryId;

             _unitOfWork.Repository<Product>().Detach(prod);

            var product = _mapper.Map<ProductViewModel, Product>(productVM);

            await _unitOfWork.Repository<Product>().DeleteAsync(product);
            int count = _unitOfWork.Complete();

            if (count > 0 && productVM.Prod_ImageName is not null)
                DocumentSettings.DeleteFile(productVM.Prod_ImageName, "Departments\\Categories\\Products");


            return RedirectToAction(nameof(Index), routeValues: new { SubCategoryId   = SubCatId, CategoryId= catId });
        }


    }







}
