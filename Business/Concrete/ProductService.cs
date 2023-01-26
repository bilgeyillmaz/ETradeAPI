using Business.Abstract;
using Core.Business;
using Core.DataAccess;
using Core.Entities;
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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IEntityRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _productRepository = productRepository; 
        }

        public Task<Product> GetByNameAsync(string name)
        {
            var hasProduct = GetAllAsync().Result.FirstOrDefault(x => x.Name != null && x.Name.ToLower().Contains(name.ToLower()));
            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({name}) not found");
            }
            return Task.FromResult(hasProduct);
        }

        public async Task<CustomResponseDto<List<ProductDetailDto>>> GetProductsWithCategory()
        {
            return await _productRepository.GetProductDetailAsync();
        }
    }
}
