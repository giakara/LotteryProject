
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.Services.Interfaces
{
	public interface ILotteryService
	{
		Task<IEnumerable<Lottery>> GetAllLotteries(CancellationToken cancellationToken = default);
		Task<Lottery> CreateLottery(AddEditLotteryDTO lottery, CancellationToken cancellationToken = default);
		Task<Lottery> GetLottery(Guid lotteryId, CancellationToken cancellationToken = default);
		Task<bool> DeleteLotteryById(Guid lotteryID, CancellationToken cancellationToken = default);
		Task<PagedList<Lottery>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default);
	}
}
