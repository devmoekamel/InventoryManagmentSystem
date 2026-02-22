using AutoMapper;
using InventoryManagmentSystem.Core.DTO.Products;
using InventoryManagmentSystem.Core.Models;
namespace InventoryManagmentSystem.Core.DTO.Products
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {

            CreateMap<Product, ProductDTO>()
              .ForMember(des => des.Category, src => src.MapFrom(p => p.Category.Name));

            CreateMap<Product, ProductCreateUpdateDTO>();

            CreateMap<ProductCreateUpdateDTO, Product>();
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductDTO, ProductCreateUpdateDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());
        }
    }
}


