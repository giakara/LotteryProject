using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LotteryProject.EFCore.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly EntityDbContext.DataContext _dbContext;

        public LotteryService(EntityDbContext.DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Lottery>> GetAllLotteries(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Lotteries.ToListAsync(cancellationToken);
        }
        public async Task<bool> DeleteLotteryById(Guid lotteryId, CancellationToken cancellationToken = default)
        {
            var dbLottery = await _dbContext.Lotteries.FindAsync(lotteryId);
            if (dbLottery == null)
                throw new LotteryNotFoundException("Lottery not found");

            _dbContext.Lotteries.Remove(dbLottery);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<Lottery> CreateLottery(AddEditLotteryDTO lottery, CancellationToken cancellationToken = default)
        {
            if (lottery == null)
                throw new LotteryNotFoundException("Lottery not found");
            if (lottery.PresentID == Guid.Empty)
                throw new LotteryPresentIsRequiredException("Present is required for the Lottery");
            if (_dbContext.Lotteries.Any(l => l.PresentID == lottery.PresentID))
                throw new LotteryPresentIsGiftedException("Present is Gifted Already");
            var guestIdsWithLotteryToday = await _dbContext.Lotteries
                                            .Where(l => l.LotteryDate.HasValue && l.LotteryDate.Value.Date == DateTime.Today)
                                            .Select(l => l.GuestID)
                                            .ToListAsync(cancellationToken);

            // Now, get all GuestIDs that don't have a record for the current day
            var availableGuestIds = await _dbContext.Guests
                                                        .Where(g => !guestIdsWithLotteryToday.Contains(g.Id))
                                                        .Select(g => g.Id)
                                                        .ToListAsync(cancellationToken);
            if (availableGuestIds.Count == 0)
            {
                throw new LotteryNoAvailableGuestsException("No available guests for the lottery today");
            }
            var lotteryToCreate = new Lottery
            {
                GuestID = availableGuestIds[new Random().Next(0, availableGuestIds.Count)],
                PresentID = lottery.PresentID,
                LotteryDate = DateTime.Now,
            };
            _dbContext.Lotteries.Add(lotteryToCreate);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return lotteryToCreate;
        }
    }
}
