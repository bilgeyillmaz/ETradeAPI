using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class ProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(CoinoCaseDbContext context) : base(context)
        {
        }

        public Task<CustomResponseDto<List<ProductDetailDto>>> GetProductDetailAsync()
        {
            using (CoinoCaseDbContext context = new CoinoCaseDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CoinoCaseDbContext>()))
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 CategoryName = c.Name,
                                 CreatedDate = p.CreatedDate,
                                 ProductName = p.Name,
                                 ProductPrice = p.Price,
                                 ProductQuantity = p.Quantity
                             };
                return Task.FromResult(new CustomResponseDto<List<ProductDetailDto>> { Data = result.ToList()});
            }
            
        }
    }
}
