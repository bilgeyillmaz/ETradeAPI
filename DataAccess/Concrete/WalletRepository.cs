using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class WalletRepository : EfEntityRepositoryBase<Wallet>, IWalletRepository
    {
        public WalletRepository(CoinoCaseDbContext context) : base(context)
        {
        }

        public Task<CustomResponseDto<List<WalletDetailDto>>> GetWalletDetailAsync()
        {
            using (CoinoCaseDbContext context = new CoinoCaseDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CoinoCaseDbContext>()))
            {
                var result = from w in context.Wallets
                             join r in context.RegisterModels
                             on w.IdentityUserId equals r.IdentityUserId
                             select new WalletDetailDto
                             {
                                 Id = w.Id,
                                 UserName = r.Username,
                                 WalletBalance =w.Balance
                             };
                return Task.FromResult(new CustomResponseDto<List<WalletDetailDto>> { Data = result.ToList() });
            }
        }
    }
}
