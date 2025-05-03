using AutoMapper;
using InventoryClassLibrary.DTO.Warehouses;
using InventoryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.DTO.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() {

            CreateMap<Category, CategoryDTO>();

        }
    }
}
