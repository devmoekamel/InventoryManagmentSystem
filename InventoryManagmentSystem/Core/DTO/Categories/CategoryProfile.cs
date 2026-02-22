using AutoMapper;
using InventoryManagmentSystem.Core.Models;

namespace InventoryManagmentSystem.Core.DTO.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() {

            CreateMap<Category, CategoryDTO>();

        }
    }
}
