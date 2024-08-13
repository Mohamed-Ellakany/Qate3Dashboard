using AutoMapper;
using Microsoft.Extensions.Configuration;
using Qate3Dashboard.DTOs;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3_Api.Helper
{
    public class DepartmentImageResolver : IValueResolver<Department, DepartmentDto, string>
    {
        private readonly IConfiguration _configuration;

        public DepartmentImageResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(Department source, DepartmentDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.Dept_ImageName))
            {
                return $"{_configuration["ApiBaseUrl"]}Images/AppImages/Departments/{source.Dept_ImageName}";
            }
            return string.Empty;
        }
    }
}

