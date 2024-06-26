using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.MappingProfiles
{
    public class SubCategoryProfile:Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory , SubCategoryViewModel>().ReverseMap();
        }
    }
}
