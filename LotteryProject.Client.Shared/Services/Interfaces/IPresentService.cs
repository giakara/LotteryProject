
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.Services.Interfaces
{
	public interface IPresentService
	{
		Task<Present> AddPresent(AddPresentDTO present, CancellationToken cancellationToken = default);
		Task<bool> DeletePresentById(Guid presentID, CancellationToken cancellationToken = default);
		Task<IEnumerable<Present>> GetAllPresents(CancellationToken cancellationToken = default);
		Task<Present> GetPresent(Guid presentId, CancellationToken cancellationToken = default);
		Task<Present> UpdatePresent(Present present, CancellationToken cancellationToken = default);
		Task<PagedList<Present>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default);
		Task<PagedList<Present>> SearchPresents(string searchText, PagingParameters pagingParameters, CancellationToken cancellationToken = default);
	}
}
