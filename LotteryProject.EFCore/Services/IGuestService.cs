using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.EFCore.Services
{
	public interface IGuestService
	{
		Task<Guest> AddGuest(AddGuestDTO guest, CancellationToken cancellationToken = default);
		Task<bool> DeleteGuestById(Guid guestID, CancellationToken cancellationToken = default);
		Task<IEnumerable<Guest>> GetAllGuests(CancellationToken cancellationToken = default);
		Task<Guest> GetGuest(Guid guestId, CancellationToken cancellationToken = default);
		Task<Guest> UpdateGuest(Guest guest, CancellationToken cancellationToken = default);
		Task<PagedList<Guest>> GetAllGuestsPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default);
		Task<PagedList<Guest>> SearchGuests(string searchText, PagingParameters pagingParameters, CancellationToken cancellationToken = default);

	}
}
