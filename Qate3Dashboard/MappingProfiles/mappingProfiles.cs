using AutoMapper;
using Qate3_Api.Helper;
using Qate3DAL.Models;
using Qate3Dashboard.DTOs;
using Qate3Dashboard.Helpers;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.MappingProfiles
{
    public class mappingProfiles : Profile
    {

        public mappingProfiles()
        {
            CreateMap<SubCategory, SubCategoryViewModel>()
                .ReverseMap();


            CreateMap<Product, ProductViewModel>()
                .ReverseMap();


            CreateMap<Department, DepartmentViewModel>()
                .ReverseMap(); ;


            CreateMap<Category, CategoriesViewModel>()
                .ReverseMap();

            CreateMap<SubCategory, SubCategoryDto>()
                .ReverseMap();


            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Prod_ImageName, opt => opt.MapFrom<ProductImageResolver>())
                .ForMember(dest => dest.SubcategoryName , o =>o.MapFrom(s=>s.Subcategory.Title))
                .ForMember(dest => dest.CategoryName , o =>o.MapFrom(s=>s.category.Cat_Title))
                .ReverseMap();



            CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.Dept_ImageName, s => s.MapFrom<DepartmentImageResolver>())
               .ReverseMap();


            CreateMap<Category, CategoryDto>()
             .ForMember(dest => dest.Cat_ImageName, opt => opt.MapFrom<CategoryImageResolver>())
             .ForMember(dest=>dest.Dept_Name , opt =>opt.MapFrom(c=>c.Department.Dept_Title))
                 .ReverseMap();
        }


    }
}
