using AutoMapper;
using InventoryClassLibrary.DTO.Products;
using InventoryClassLibrary.Models;
namespace InventoryManagmentSystem.DTO.Products;

    public class ProductProfile : Profile
{

    public ProductProfile()
    {

        CreateMap<Product, ProductDTO>()
          .ForMember(des=>des.Category,src=>src.MapFrom(p=>p.Category.Name));

        CreateMap<Product, ProductCreateUpdateDTO>();

        CreateMap<ProductCreateUpdateDTO, Product>();
        CreateMap<ProductDTO, Product>();
        CreateMap<ProductDTO, ProductCreateUpdateDTO>()
    .ForMember(dest => dest.CategoryId, opt => opt.Ignore());


    }

}


