﻿

using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;

namespace LotteryProject.EFCore.Services
{
	public interface ILotteryService
	{
		Task<Lottery> GetLottery(Guid lotteryID, CancellationToken cancellationToken = default);
		Task<IEnumerable<Lottery>> GetAllLotteries(CancellationToken cancellationToken = default);
		Task<Lottery> CreateLottery(AddEditLotteryDTO lottery, CancellationToken cancellationToken = default);

		Task<bool> DeleteLotteryById(Guid lotteryID, CancellationToken cancellationToken = default);

		Task<PagedList<Lottery>> GetAllLotteriesPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default);
	}
}
