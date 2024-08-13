using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;

namespace Qate3Dashboard.Helpers
{
        public class CategoryImageResolver : IValueResolver<Category    , CategoryDto, string>
        {
            private readonly IConfiguration _configuration;

            public CategoryImageResolver(IConfiguration configuration)
            {
                _configuration = configuration;
            }


            public string Resolve(Category source, CategoryDto destination, string destMember, ResolutionContext context)
            {

                if (!string.IsNullOrEmpty(source.Cat_ImageName))
                {
                    return $"{_configuration["ApiBaseUrl"]}Images/AppImages/Departments/Categories/{source.Cat_ImageName}";
                }
                return string.Empty;
            }
        }

}
