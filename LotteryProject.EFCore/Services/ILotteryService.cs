

using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;

namespace LotteryProject.EFCore.Services
{
    public interface ILotteryService
    {
        Task<IEnumerable<Lottery>> GetAllLotteries(CancellationToken cancellationToken = default);
        Task<Lottery> CreateLottery(AddEditLotteryDTO lottery, CancellationToken cancellationToken = default);

        Task<bool> DeleteLotteryById(Guid lotteryID, CancellationToken cancellationToken = default);

    }
}
