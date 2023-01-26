using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CartsProductRepository : EfEntityRepositoryBase<CartsProduct>, ICartsProductRepository
    {
        public CartsProductRepository(CoinoCaseDbContext context) : base(context)
        {
        }
    }
}
