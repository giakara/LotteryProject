
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.Services.Interfaces
{
	public interface IGuestService
	{
		Task<Guest> AddGuest(AddGuestDTO guest, CancellationToken cancellationToken = default);
		Task<bool> DeleteGuestById(Guid guestID, CancellationToken cancellationToken = default);
		Task<IEnumerable<Guest>> GetAllGuests(CancellationToken cancellationToken = default);
		Task<Guest> GetGuest(Guid guestId, CancellationToken cancellationToken = default);
		Task<Guest> UpdateGuest(Guest guest, CancellationToken cancellationToken = default);
		Task<PagedList<Guest>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default);
	}
}
