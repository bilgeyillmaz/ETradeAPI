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
    public class ShoppingCartRepository : EfEntityRepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(CoinoCaseDbContext context) : base(context)
        {
        }

        public Task<CustomResponseDto<List<ShoppingCartDetailDto>>> GetShoppingCartDetailAsync()
        {
            using (CoinoCaseDbContext context = new CoinoCaseDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CoinoCaseDbContext>()))
            {
                var result = from s in context.ShoppingCarts
                             join r in context.RegisterModels
                             on s.IdentityUserId equals r.IdentityUserId
                             select new ShoppingCartDetailDto
                             {
                                 Id = s.Id,
                                 CreatedDate = s.CreatedDate,
                                 CustomerName = r.Username,
                                 ShoppingCartPrice = s.Price,
                                 ShoppingCartProducts = s.Products,
                                 CountOfProducts= s.Products.Count, 
                                 IdentityUserId = r.IdentityUserId
                             };
                return Task.FromResult(new CustomResponseDto<List<ShoppingCartDetailDto>> { Data = result.ToList()});
            }
        }
    }
}
