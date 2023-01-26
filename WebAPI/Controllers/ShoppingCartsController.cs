using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Authorize]
    public class ShoppingCartsController : CustomBaseController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var shoppingCarts = await _shoppingCartService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<ShoppingCart>>.Success(200, shoppingCarts));
        }

        [HttpGet("GetCustomersShoppingCartDetails")]
        public async Task<IActionResult> GetShoppingCartDetails()
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            var userCartDto = await _shoppingCartService.GetShoppingCartDetails(currentUserId);
            return CreateActionResult(CustomResponseDto<ShoppingCartDetailDto>.Success(200, userCartDto));
        }

        [HttpGet("AddProductToCustomerShoppingChart")]
        public async Task<IActionResult> AddProductToCustomerShoppingChart(int productId, int quantity)
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            var shoppingCartDetailDto = await _shoppingCartService.AddProductToShoppingCartIsStock(currentUserId, productId, quantity);
            return CreateActionResult(CustomResponseDto<ShoppingCartDetailDto>.Success(200, shoppingCartDetailDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<ShoppingCart>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var shoppingCart = await _shoppingCartService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<ShoppingCart>.Success(200, shoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ShoppingCart shoppingChart)
        {
            await _shoppingCartService.AddAsync(shoppingChart);
            return CreateActionResult(CustomResponseDto<ShoppingCart>.Success(201, shoppingChart));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ShoppingCart shoppingChart)
        {
            await _shoppingCartService.UpdateAsync(shoppingChart);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpPost("RemoveProductsFromCustomerShoppingCart")]
        public async Task<IActionResult> UpdateProductsOfShoppingCartByCustomer(int productId, int quantity)
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            if(quantity ==0)
            {
                quantity= 1;    
            }
            await _shoppingCartService.UpdateShoppingCart(currentUserId, productId, quantity);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var shoppingCart = await _shoppingCartService.GetByIdAsync(id);

            if (shoppingCart == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
            }
            await _shoppingCartService.RemoveAsync(shoppingCart);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
