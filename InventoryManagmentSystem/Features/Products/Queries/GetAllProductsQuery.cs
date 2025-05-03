using AutoMapper.QueryableExtensions;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using InventoryClassLibrary.Services;
using InventoryManagmentSystem.DTO.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Features.Products.Queries
{
    public class GetAllProductsQuery :IRequest<IEnumerable<ProductDTO>>
    {
    }


    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery,IEnumerable<ProductDTO>>
    {
        private readonly IGenericRepository<Product> _productRepo;

        public GetAllProductsQueryHandler(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
          return   _productRepo.GetAll().ProjectTo<ProductDTO>().ToList();
        }
    }
}
