using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService : IService<Order>
    {
        Task<CustomResponseDto<List<OrderDetailDto>>> GetOrderWithUserAndShoppingCart();
        Task<OrderDetailDto> CreateOrder(string cUserId);
        Task<CustomResponseDto<List<OrderDetailDto>>> GetAllOrdersByCustomerInfo(string currentUserId);
    }
}
