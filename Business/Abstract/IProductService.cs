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
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductDetailDto>>> GetProductsWithCategory();
        Task<Product> GetByNameAsync(string name);
    }
}
