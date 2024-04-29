using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using LotteryProject.Models.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.EFCore.Services
{
	public class PresentService : IPresentService
	{
		private readonly EntityDbContext.DataContext _dbContext;

		public PresentService(EntityDbContext.DataContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<Present>> GetAllPresents(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Presents.ToListAsync(cancellationToken);
		}
		public async Task<Present> GetPresent(Guid presentId, CancellationToken cancellationToken = default)
		{
			var present = await _dbContext.Presents.FindAsync(presentId, cancellationToken);

			if (present is null)
			{
				throw new PresentNotFoundException("Present not found");
			}

			return present;

		}
		public async Task<Present> AddPresent(AddPresentDTO present, CancellationToken cancellationToken = default)
		{
			var presentToAdd = new Present(present.Description)
			{
				Category = present.Category,
			};
			if (presentToAdd == null)
			{
				throw new PresentNotFoundException("Present not found");
			}
			_dbContext.Presents.Add(presentToAdd);
			await _dbContext.SaveChangesAsync();
			return presentToAdd;
		}
		public async Task<Present> UpdatePresent(Present present, CancellationToken cancellationToken = default)
		{
			var dbPresent = await _dbContext.Presents.FindAsync(present.Id);
			if (dbPresent == null)
				throw new PresentNotFoundException("Present not found");
			dbPresent.Description = present.Description;
			dbPresent.Category = present.Category;
			await _dbContext.SaveChangesAsync();
			return dbPresent;
		}
		public async Task<bool> DeletePresentById(Guid presentId, CancellationToken cancellationToken = default)
		{
			var dbPresent = await _dbContext.Presents.FindAsync(presentId);
			if (dbPresent == null)
				throw new PresentNotFoundException("Present not found");

			_dbContext.Presents.Remove(dbPresent);
			await _dbContext.SaveChangesAsync();

			return true;
		}
		public async Task<PagedList<Present>> GetAllPresentsPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			var dbSet = _dbContext.Set<Present>();
			var countDBItems = await dbSet.CountAsync(cancellationToken);
			var lastPage = (int)Math.Ceiling((double)countDBItems / pagingParameters.PageSize);
			if (pagingParameters.PageNumber > lastPage && lastPage > 0)
				pagingParameters.PageNumber = lastPage;
			var items = await dbSet
						.OrderBy(on => on.Description)
						.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
						.Take(pagingParameters.PageSize)
						.ToListAsync();

			var pagedList = new PagedList<Present>()
			{
				Items = items,
				PageNumber = pagingParameters.PageNumber,
				PageSize = pagingParameters.PageSize,
				TotalCount = countDBItems,
			};

			return new PagedList<Present>()
			{
				Items = pagedList.Items,
				TotalCount = pagedList.TotalCount,
				PageNumber = pagedList.PageNumber,
				PageSize = pagedList.PageSize,
			};
		}
		public async Task<PagedList<Present>> SearchPresents(string searchText, PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			var dbSet = _dbContext.Set<Present>();
			var countDBItems = await dbSet.Where(p => p.Description.Contains(searchText)).CountAsync(cancellationToken);
			var lastPage = (int)Math.Ceiling((double)countDBItems / pagingParameters.PageSize);
			if (pagingParameters.PageNumber > lastPage && lastPage > 0)
				pagingParameters.PageNumber = lastPage;
			var items = await dbSet
						.Where(p => p.Description.Contains(searchText))
						.OrderBy(on => on.Description)
						.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
						.Take(pagingParameters.PageSize)
						.ToListAsync();

			var pagedList = new PagedList<Present>()
			{
				Items = items,
				PageNumber = pagingParameters.PageNumber,
				PageSize = pagingParameters.PageSize,
				TotalCount = countDBItems,
			};

			return new PagedList<Present>()
			{
				Items = pagedList.Items,
				TotalCount = pagedList.TotalCount,
				PageNumber = pagedList.PageNumber,
				PageSize = pagedList.PageSize,
			};
		}
	}
}
