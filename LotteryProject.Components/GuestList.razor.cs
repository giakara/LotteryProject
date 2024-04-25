using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Components
{
	public partial class GuestList : ComponentBase
	{

		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
		private IEnumerable<Guest> guests { get; set; } = null!;
		private List<int> indexes { get; set; } = new List<int>();
		private PagingParameters _pagingParameters = new PagingParameters() { PageSize = 5 }; //appsettings

		protected override async Task OnInitializedAsync()
		{
			await Task.Delay(500);
			await GetGuestsFromDb();
		}
		private async Task GetGuestsFromDb()
		{
			var pagedGuests = await _guestService.GetPaged(_pagingParameters);
			indexes = new List<int>();
			guests = pagedGuests.Items.ToList();
			_pagingParameters.TotalPages = pagedGuests.TotalPages;
			for (int i = 1; i < guests.Count() + 1; i++)
				indexes.Add(pagedGuests.PageSize * (pagedGuests.PageNumber - 1) + i);
		}

		private async Task HandleGuestDeleted(Guest guest)
		{
			await GetGuestsFromDb();
		}
		protected void AddGuest()
		{
			NavigationManager.NavigateTo("/AddGuest");
		}
		private async Task SelectedPage(int page)
		{
			_pagingParameters.PageNumber = page;
			await GetGuestsFromDb();
		}
	}
}
