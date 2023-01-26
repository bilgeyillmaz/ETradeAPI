using Business.Abstract;
using Core.DataAccess;
using Core.UnitOfWork;
using Core.Utilities.Exceptions;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShoppingCartService : Service<ShoppingCart>, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingRepository;
        private readonly ICartsProductRepository _cartsProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRegisterModelRepository _registerModel;
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartService(IEntityRepository<ShoppingCart> repository, IUnitOfWork unitOfWork, IShoppingCartRepository shoppingRepository,
            IProductRepository productRepository, ICartsProductRepository cartsProductRepository, IRegisterModelRepository registerModel) : base(repository, unitOfWork)
        {
            _shoppingRepository = shoppingRepository;
            _cartsProductRepository = cartsProductRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _registerModel = registerModel;
        }

        public async Task<ShoppingCartDetailDto> AddProductToShoppingCartIsStock(string cUserId, int productId, int quantity)
        {
            ShoppingCartDetailDto shoppingCartDetailDto = new ShoppingCartDetailDto();
            var shoppingCart = GetAllAsync().Result.FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (shoppingCart == null)
            {
                ShoppingCart generateShoppingCart = new ShoppingCart();
                generateShoppingCart.IdentityUserId = cUserId;
                await _shoppingRepository.AddAsync(generateShoppingCart);
                await _unitOfWork.CommitAsync();
                shoppingCart = generateShoppingCart;
            }
            var product = _productRepository.GetAll().FirstOrDefault(x => x.Id == productId);
            if (product == null)
                throw new NotFoundException("Product not found");
            var cUser = _registerModel.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (cUser == null)
                throw new NotFoundException("User not found");

            if (cUser != null && product != null && product.Quantity - quantity >= 0)
            {
                for (int i = 0; i < quantity; i++)
                {
                    if (shoppingCart != null)
                    {
                        CartsProduct cartsProduct = new CartsProduct()
                        {
                            ProductId = product.Id,
                            ShoppingCartId = shoppingCart.Id
                        };
                        await _cartsProductRepository.AddAsync(cartsProduct);
                        if (shoppingCart.Products == null)
                        {
                            shoppingCart.Products = new List<Product>();
                        }
                        shoppingCart.Products.Add(product);
                        if(shoppingCart.Price == null)
                        {
                            shoppingCart.Price = product.Price;
                        }
                        else
                        {
                            shoppingCart.Price += product.Price;
                        }
                        _shoppingRepository.Update(shoppingCart);
                        await _unitOfWork.CommitAsync();
                    }
                }
                shoppingCartDetailDto.ShoppingCartPrice = shoppingCart?.Price;
                shoppingCartDetailDto.CustomerName = cUser?.Username;
                shoppingCartDetailDto.ShoppingCartProducts = shoppingCart?.Products;
                shoppingCartDetailDto.CreatedDate = shoppingCart?.CreatedDate;
                shoppingCartDetailDto.CountOfProducts = shoppingCart?.Products?.Count;
            }

            return shoppingCartDetailDto;

        }

        public Task<ShoppingCartDetailDto> GetShoppingCartDetails(string cUserId)
        {
            var shoppingCarts = _shoppingRepository.GetShoppingCartDetailAsync().Result;
            if (shoppingCarts == null )
                throw new NotFoundException("Shopping Carts not found");
            var userCartId = _shoppingRepository.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId)?.Id;
            if (userCartId == null)
                throw new NotFoundException("Shopping Cart not found");
            if (shoppingCarts.Data == null)
                throw new NotFoundException("Shopping Cart not found");
            var userCartDto = shoppingCarts.Data.FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (userCartDto == null)
            {
                throw new NotFoundException("Shopping Cart not found");
            }
            return Task.FromResult(userCartDto);
        }

        public async Task UpdateShoppingCart(string cUserId, int productId, int quantity)
        {
            var userCart = _shoppingRepository.GetAll().FirstOrDefault(x => x.IdentityUserId == cUserId);
            if (userCart == null)
                throw new NotFoundException("Shopping Cart not found");
            var product = _productRepository.GetAll().FirstOrDefault(x=>x.Id == productId);
            if (product == null)
                throw new NotFoundException("Product not found");
            if (userCart.Products !=null)
            {
                for(var i=0; i< quantity; i++)
                {
                    if(userCart.Products.Any(x=>x.Id == product.Id))
                    {
                        userCart.Products.Remove(product);
                        _shoppingRepository.Update(userCart);   
                        await _unitOfWork.CommitAsync();   
                    }
                    
                }
            }
        }
    }
}
