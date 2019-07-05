using AutoMapper;
using ProductTestServer.Common.Models;
using ProductTestServer.Web.ViewModels.Product;

namespace ProductTestServer.Web.MapperConfiguration
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductVm>().ReverseMap();
        }
    }
}
