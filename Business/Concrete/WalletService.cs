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
    public class WalletService : Service<Wallet>, IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;
        public WalletService(IEntityRepository<Wallet> repository, IUnitOfWork unitOfWork, IWalletRepository walletRepository) : base(repository, unitOfWork)
        {
            _walletRepository = walletRepository;
            _unitOfWork= unitOfWork;    
        }


        public Task<CustomResponseDto<List<WalletDetailDto>>> GetAllWalletsWithUserDetail()
        {
            return _walletRepository.GetWalletDetailAsync();
        }

        public async Task<WalletDetailDto> UpdateCustomerWalletBallance(double walletBalance, string currentUserId)
        {
            var wallet= _walletRepository.GetAll().FirstOrDefault(x=>x.IdentityUserId == currentUserId);    
            if(wallet == null)
                throw new NotFoundException("Wallet not found");
            wallet.Balance = walletBalance;
             _walletRepository.Update(wallet);
            await _unitOfWork.CommitAsync();
            var allWalletsDto = await _walletRepository.GetWalletDetailAsync();
            if (allWalletsDto.Data == null)
                throw new NotFoundException("Wallet not found");
            var walletDto = allWalletsDto.Data.FirstOrDefault(x => x.Id == wallet.Id);
            if(walletDto == null)
                throw new NotFoundException("Wallet not found");
            return walletDto;

        }
    }
}
