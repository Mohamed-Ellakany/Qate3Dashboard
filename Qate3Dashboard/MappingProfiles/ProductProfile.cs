using AutoMapper;
using Qate3DAL.Models;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
          CreateMap<Product, ProductViewModel>().ReverseMap();

        }
    }
}
