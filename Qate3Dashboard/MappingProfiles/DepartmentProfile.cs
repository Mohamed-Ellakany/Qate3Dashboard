using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.MappingProfiles
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap() ;
        }
    }
}
