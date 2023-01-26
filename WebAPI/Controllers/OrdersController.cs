using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Authorize]
    public class OrdersController : CustomBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var orders = await _orderService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<Order>>.Success(200, orders));
        }

        [HttpGet("CreateCustomerOrder")]
        public async Task<IActionResult> CreateCustomerOrder()
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            var orderDetailDto = await _orderService.CreateOrder(currentUserId);
            return CreateActionResult(CustomResponseDto<OrderDetailDto>.Success(200, orderDetailDto));
        }
        [HttpGet("GetOrderWithUserAndShoppingCartDetails")]
        public async Task<IActionResult> GetOrderWithUserAndShoppingCartDetails()
        {
            var orderDetailDtos = await _orderService.GetOrderWithUserAndShoppingCart();
            if(orderDetailDtos==null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Orders not found"));
            return Ok(orderDetailDtos);
        }

        [HttpGet("GetAllOrdersOfCustomer")]
        public async Task<IActionResult> GetAllOrdersOfCustomer()
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            var orderDetailDtos = await _orderService.GetAllOrdersByCustomerInfo(currentUserId);
            if (orderDetailDtos == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Orders not found"));
            return Ok(orderDetailDtos);
        }

        [ServiceFilter(typeof(NotFoundFilter<Wallet>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Order>.Success(200, order));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Order order)
        {
            await _orderService.AddAsync(order);
            return CreateActionResult(CustomResponseDto<Order>.Success(201, order));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            await _orderService.UpdateAsync(order);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
            }
            await _orderService.RemoveAsync(order);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
