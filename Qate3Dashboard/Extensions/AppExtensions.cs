using Qate3BLL;
using Qate3BLL.Interfaces;
using Qate3BLL.Repositories;
using Qate3DAL.Models;
using Qate3Dashboard.MappingProfiles;

namespace Qate3Dashboard.Extensions
{
    public static class AppExtensions 
    {
        public static IServiceCollection AppExtensionServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(M=>M.AddProfile(new DepartmentProfile()));
            services.AddAutoMapper(M=>M.AddProfile(new CategoryProfile()));
            services.AddAutoMapper(M=>M.AddProfile(new SubCategoryProfile()));
            services.AddAutoMapper(M=>M.AddProfile(new ProductProfile()));
             
            return  services;
        }

    }
}
