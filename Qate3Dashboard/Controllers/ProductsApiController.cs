using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qate3BLL.Interfaces;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;
using Qate3Dashboard.Errors;

namespace Qate3Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsApiController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInCategory(int? CategoryId, int? SubCategoryId)
        {
            if (CategoryId == null || CategoryId == 0)
            {
                return BadRequest(new ApiResponse(400));
            }

            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(CategoryId);
            if (category == null)
            {
                return NotFound(new ApiResponse(404, "no Category with this id"));
            }

            if (SubCategoryId == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            if (SubCategoryId > 2 || SubCategoryId < 1)
            {
                return NotFound(new ApiResponse(404, "no SubCategory with this id"));
            }


            var Products = _unitOfWork.Repository<Product>().GetAllAsync().Result.Where(p => p.SubcategoryId == SubCategoryId && p.categoryId == CategoryId);


            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);


            return Ok(mappedProducts);
        }

        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> ProductSearch(string? Search)
        {
            if (string.IsNullOrEmpty(Search))
            {
                var DefaultProducts = _unitOfWork.Repository<Product>().GetAllAsync().Result.Take(10);

                var MappedDefaultProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(DefaultProducts);

                return Ok(MappedDefaultProducts);
            }
            else
            {
                var GetSearchedProducts = _unitOfWork.Repository<Product>().GetAllAsync().Result.Where(P => P.Prod_Title.ToLower().Trim().Contains(Search.ToLower().Trim()));

                if(!GetSearchedProducts.Any())
                {
                    return NotFound( new ApiResponse (404 , "No Product With This Name"));
                }

                var MappedSearchedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(GetSearchedProducts);

                return Ok(MappedSearchedProducts);

            }



        }









    }
}
