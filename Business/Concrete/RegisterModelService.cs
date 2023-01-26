using Business.Abstract;
using Core.DataAccess;
using Core.UnitOfWork;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RegisterModelService : Service<RegisterModel>, IRegisterModelService
    {
        private readonly IRegisterModelRepository _registerModelRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterModelService(IEntityRepository<RegisterModel> repository, IUnitOfWork unitOfWork, IRegisterModelRepository registerModelRepository,
            IShoppingCartRepository shoppingCartRepository, IWalletRepository walletRepository) : base(repository, unitOfWork)
        {
            _registerModelRepository = registerModelRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterModel> AddWithShoppingCartAndWalletAsync(RegisterModel entity)
        {
            await _registerModelRepository.AddAsync(entity);
            ShoppingCart shoppingCart = new()
            {
                IdentityUserId = entity.IdentityUserId,
                CreatedDate = DateTime.Now
            };
            await _shoppingCartRepository.AddAsync(shoppingCart);
            Wallet wallet = new()
            {
                IdentityUserId = entity.IdentityUserId,
                CreatedDate = DateTime.Now
            };
            await _walletRepository.AddAsync(wallet);
            await _unitOfWork.CommitAsync();
            return entity;
        }
    }
}
