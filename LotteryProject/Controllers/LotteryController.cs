using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Mvc;

namespace LotteryProject.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LotteryController : ControllerBase
	{
		private readonly ILotteryService _lotteryService;
		public LotteryController(ILotteryService lotteryService)
		{
			_lotteryService = lotteryService;
		}
		[HttpGet]
		[Route("GetLottery/{lotteryID:guid}")]
		public async Task<IActionResult> GetById(Guid lotteryID, CancellationToken cancellationToken = default)
		{
			var lottery = await _lotteryService.GetLottery(lotteryID, cancellationToken);
			return Ok(lottery);
		}
		[HttpGet]
		[Route("GetAllLotteries")]
		public async Task<IActionResult> GetAllLotteries(CancellationToken cancellationToken = default)
		{
			var allLotteries = await _lotteryService.GetAllLotteries(cancellationToken);
			return Ok(allLotteries);
		}
		[HttpPost]
		[Route("CreateLottery")]
		public async Task<IActionResult> CreateLottery([FromBody] AddEditLotteryDTO lottery, CancellationToken cancellationToken = default)
		{
			var lotteryToAdd = await _lotteryService.CreateLottery(lottery, cancellationToken);

			return Ok(lotteryToAdd);
		}
		[HttpDelete]
		[Route("DeleteLottery/{lotteryID:guid}")]
		public async Task<IActionResult> DeleteById(Guid lotteryID, CancellationToken cancellationToken = default)
		{
			await _lotteryService.DeleteLotteryById(lotteryID, cancellationToken);

			return Ok();
		}
		[HttpGet]
		[Route("GetAllLotteriesPaged")]
		public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			var allLotteries = await _lotteryService.GetAllLotteriesPaged(pagingParameters, cancellationToken);
			return Ok(allLotteries);
		}
	}
}
