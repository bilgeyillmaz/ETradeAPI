using Core.Business;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShoppingCartService:IService<ShoppingCart>
    {
        Task<ShoppingCartDetailDto> AddProductToShoppingCartIsStock(string cUserId, int productId, int quantity);
        Task<ShoppingCartDetailDto> GetShoppingCartDetails(string cUserIdy);
        Task UpdateShoppingCart(string cUserId, int productId, int quantity);
    }
}
