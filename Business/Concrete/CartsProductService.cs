using Business.Abstract;
using Core.DataAccess;
using Core.UnitOfWork;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartsProductService : Service<CartsProduct>, ICartsProductService
    {
        private readonly ICartsProductRepository _cartsProductRepository;

        public CartsProductService(IEntityRepository<CartsProduct> repository, IUnitOfWork unitOfWork, ICartsProductRepository cartsProductRepository) : base(repository, unitOfWork)
        {
            _cartsProductRepository = cartsProductRepository;
        }
    }
}
