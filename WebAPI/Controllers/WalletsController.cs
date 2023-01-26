using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Authorize]
    public class WalletsController : CustomBaseController
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var wallets = await _walletService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<Wallet>>.Success(200, wallets));
        }

        [HttpGet("GetAllWalletsWithUserDetails")]
        public async Task<IActionResult> GetAllWalletsWithUserDetails()
        {
            var wallets = await _walletService.GetAllWalletsWithUserDetail();
            if(wallets == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item."));
            return Ok(wallets);
        }

        [HttpGet("UpdateCustomerWalletBallance")]
        public async Task<IActionResult> UpdateCustomerWalletBallance(double walletBalance)
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (currentUserId == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "User not found"));
            var wallet = await _walletService.UpdateCustomerWalletBallance(walletBalance, currentUserId);
            if (wallet == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item."));
            return CreateActionResult(CustomResponseDto<WalletDetailDto>.Success(200, wallet));
        }

        [ServiceFilter(typeof(NotFoundFilter<Wallet>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var wallet = await _walletService.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Wallet>.Success(200, wallet));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Wallet wallet)
        {
            await _walletService.AddAsync(wallet);
            return CreateActionResult(CustomResponseDto<Wallet>.Success(201, wallet));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Wallet wallet)
        {
            await _walletService.UpdateAsync(wallet);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var wallet = await _walletService.GetByIdAsync(id);

            if (wallet == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "There is no item with this Id"));
            }
            await _walletService.RemoveAsync(wallet);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

