using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var products = await _productService.GetProductsWithCategory();
            if (products == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, "There is no any data."));
            }
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<Product>>.Success(200, products));
        }

        //[ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _productService.GetByNameAsync(name);
            if (product == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, "There is no any data."));
            }
            return CreateActionResult(CustomResponseDto<Product>.Success(200, product));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Product>.Success(200, product));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Product product)
        {
            await _productService.AddAsync(product);
            return CreateActionResult(CustomResponseDto<Product>.Success(201, product));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productService.UpdateAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
            }
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
