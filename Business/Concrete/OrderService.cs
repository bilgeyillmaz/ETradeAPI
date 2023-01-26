using Business.Abstract;
using Core.DataAccess;
using Core.UnitOfWork;
using Core.Utilities.Exceptions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRegisterModelRepository _registerModelRepository;
        private readonly IUnitOfWork _unitOfWork;


        public OrderService(IEntityRepository<Order> repository, IUnitOfWork unitOfWork, IOrderRepository orderRepository, IWalletRepository walletRepository,
            IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IRegisterModelRepository registerModelRepository) : base(repository, unitOfWork)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _walletRepository = walletRepository;
            _productRepository = productRepository;
            _registerModelRepository = registerModelRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetailDto> CreateOrder(string cUserId)
        {
            OrderDetailDto orderDetailDto = new OrderDetailDto();
            Order order = new Order();
            var shoppingCart = _shoppingCartRepository.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (shoppingCart == null)
                throw new NotFoundException("Shopping Cart not found");
            var wallet = _walletRepository.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found");
            var customer = _registerModelRepository.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (customer == null)
                throw new NotFoundException("Customer not found");
            if (wallet.Balance != null && shoppingCart.Price != null && shoppingCart.Products != null)
            {
                if (wallet.Balance >= shoppingCart.Price)
                {
                    order.ShoppingCartId = shoppingCart.Id;
                    order.IdentityUserId = cUserId;
                    order.TotalPrice = shoppingCart.Price;
                    order.CreatedDate= DateTime.Now;    
                    await _orderRepository.AddAsync(order);
                    await _unitOfWork.CommitAsync();    

                    wallet.Balance -= order.TotalPrice;
                    wallet.UpdatedDate = DateTime.Now;  
                    _walletRepository.Update(wallet);
                    await _unitOfWork.CommitAsync();


                    var products = _productRepository.GetAll();
                    foreach (var item in shoppingCart.Products)
                    {
                        var product = products.FirstOrDefault(x => x.Id == item.Id);
                        if (product != null)
                        {
                            product.Quantity -= 1;
                            if (product.Quantity == 0)
                            {
                                _productRepository.Remove(product);
                                await _unitOfWork.CommitAsync();
                            }
                            _productRepository.Update(product);
                            await _unitOfWork.CommitAsync();
                        }
                        throw new NotFoundException("Product not found");
                    }
                }
            }
            else
            {
                throw new NotFoundException("Wallet Balance/ Shopping Cart Price/ Shopping Cart Products not found");
            }

            orderDetailDto.OrderNo = order.OrderNo;
            orderDetailDto.ShoppingCartProducts= shoppingCart.Products;
            orderDetailDto.CustomerPhoneNumber = customer.PhoneNumber;
            orderDetailDto.CustomerEmail = customer.EmailAddress;
            orderDetailDto.CustomerName = customer.Username;
            orderDetailDto.CreatedDate = order.CreatedDate;
            orderDetailDto.Id = order.Id;
            orderDetailDto.CustomerAddress= customer.Address;   
            orderDetailDto.TotalPrice = order.TotalPrice;   
            
            return orderDetailDto;
        }

        public async Task<CustomResponseDto<List<OrderDetailDto>>> GetAllOrdersByCustomerInfo(string currentUserId)
        {
            var orders =  GetAllAsync().Result.Where(x => x.IdentityUserId == currentUserId);
            if (orders == null)
                throw new NotFoundException("Orders not found");
            List<OrderDetailDto> orderDetailDtos = new();
            foreach (var item in orders)
            {
                var orderDetails = (await _orderRepository.GetOrderDetailAsync()).Data;
                if (orderDetails != null)
                {
                    var addedOrder = orderDetails.FirstOrDefault(x => x.Id == item.Id);
                    if(addedOrder == null)
                        throw new NotFoundException("Order not found");
                    orderDetailDtos.Add(addedOrder);
                }
            }
            if(orderDetailDtos ==null)
                throw new NotFoundException("Orders not found");
            return new CustomResponseDto<List<OrderDetailDto>>
            {
                Data = orderDetailDtos
            };
        }

        public Task<CustomResponseDto<List<OrderDetailDto>>> GetOrderWithUserAndShoppingCart()
        {
            return _orderRepository.GetOrderDetailAsync();
        }
    }
}
