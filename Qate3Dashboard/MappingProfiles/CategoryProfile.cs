using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.MappingProfiles
{
    public class CategoryProfile : Profile
    {

        public CategoryProfile()
        {
            CreateMap<Category, CategoriesViewModel>().ReverseMap();
        }
    }
}
