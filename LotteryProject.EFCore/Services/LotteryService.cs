using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using LotteryProject.Models.Paging;
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
		public async Task<Lottery> GetLottery(Guid lotteryId, CancellationToken cancellationToken = default)
		{
			var lottery = await _dbContext.Lotteries.FindAsync(lotteryId, cancellationToken);

			if (lottery is null)
			{
				throw new LotteryNotFoundException("Lottery not found");
			}

			return lottery;

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

			if (lottery.PresentID == Guid.Empty)
				throw new LotteryPresentIsRequiredException("Present is required for the Lottery");
			if (_dbContext.Lotteries.Any(l => l.PresentId == lottery.PresentID))
				throw new LotteryPresentIsGiftedException("Present is Gifted Already");
			var guestIdsWithLotteryToday = await _dbContext.Lotteries
											.Where(l => l.LotteryDate.HasValue && l.LotteryDate.Value.Date == DateTime.Today)
											.Select(l => l.GuestId)
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
			var lotteryToCreate = new Lottery(lottery.PresentID)
			{
				GuestId = availableGuestIds[new Random().Next(0, availableGuestIds.Count)],
				LotteryDate = DateTime.Now,
			};
			lotteryToCreate.Present = _dbContext.Presents.FirstOrDefault(l => l.Id == lotteryToCreate.PresentId);
			lotteryToCreate.Guest = _dbContext.Guests.FirstOrDefault(l => l.Id == lotteryToCreate.GuestId);
			_dbContext.Lotteries.Add(lotteryToCreate);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return lotteryToCreate;
		}
		public async Task<PagedList<Lottery>> GetAllLotteriesPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			var dbSet = _dbContext.Set<Lottery>();
			var countDBItems = await dbSet.CountAsync(cancellationToken);
			var lastPage = (int)Math.Ceiling((double)countDBItems / pagingParameters.PageSize);
			if (pagingParameters.PageNumber > lastPage && lastPage > 0)
				pagingParameters.PageNumber = lastPage;
			var items = await dbSet
						.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
						.Take(pagingParameters.PageSize)
						.ToListAsync();

			var pagedList = new PagedList<Lottery>()
			{
				Items = items,
				PageNumber = pagingParameters.PageNumber,
				PageSize = pagingParameters.PageSize,
				TotalCount = countDBItems,
			};

			return new PagedList<Lottery>()
			{
				Items = pagedList.Items,
				TotalCount = pagedList.TotalCount,
				PageNumber = pagedList.PageNumber,
				PageSize = pagedList.PageSize,
			};
		}
	}
}
