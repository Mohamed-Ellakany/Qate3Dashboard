using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;

namespace Qate3Dashboard.Helpers
{
    public class ProductImageResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.Prod_ImageName))
            {
                return $"{_configuration["ApiBaseUrl"]}Images/AppImages/Departments/Categories/Products/{source.Prod_ImageName}";
            }
            return string.Empty;
        }
    }


}
