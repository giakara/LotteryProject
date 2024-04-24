using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Components;
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

		private IEnumerable<Guest> guests { get; set; } = null!;

		private PagingParameters _pagingParameters = new PagingParameters() { PageSize = 5 }; //appsettings

		protected override async Task OnInitializedAsync()
		{
			await Task.Delay(500);
			await GetGuestsFromDb();
		}
		private async Task GetGuestsFromDb()
		{
			var pagedGuests = await _guestService.GetPaged(_pagingParameters);
			guests = pagedGuests.Items.ToList();
			_pagingParameters.TotalPages = pagedGuests.TotalPages;
		}

		private async Task HandleGuestDeleted(Guest guest)
		{
			await GetGuestsFromDb();
		}

		private async Task SelectedPage(int page)
		{
			_pagingParameters.PageNumber = page;
			await GetGuestsFromDb();
		}
	}
}
