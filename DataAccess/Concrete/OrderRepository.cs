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
    public class OrderRepository : EfEntityRepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(CoinoCaseDbContext context) : base(context)
        {
        }

        public Task<CustomResponseDto<List<OrderDetailDto>>> GetOrderDetailAsync()
        {
            using (CoinoCaseDbContext context = new CoinoCaseDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CoinoCaseDbContext>()))
            {
                var result = from o in context.Orders
                             join r in context.RegisterModels
                             on o.IdentityUserId equals r.IdentityUserId
                             join s in context.ShoppingCarts
                             on o.ShoppingCartId equals s.Id
                             select new OrderDetailDto
                             {
                                 Id = o.Id,
                                 CreatedDate= o.CreatedDate,
                                 CustomerName = r.Username,
                                 CustomerAddress = r.Address,
                                 CustomerEmail = r.EmailAddress,
                                 CustomerPhoneNumber= r.PhoneNumber,  
                                 ShoppingCartProducts = s.Products,
                                 TotalPrice = s.Price,
                                 OrderNo = o.OrderNo

                             };
                return Task.FromResult(new CustomResponseDto<List<OrderDetailDto>> { Data = result.ToList()});
            }
        }
    }
}
