using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
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
		private string? filteredText;

		private List<int> indexes { get; set; } = new List<int>();
		private CancellationTokenSource src = new();
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
		private async Task OnChangeTask(ChangeEventArgs args)
		{
			filteredText = args.Value.ToString();
			src.Cancel();
			src = new();
			await Task.Delay(500, src.Token).ContinueWith(SearchGuests, src.Token);

		}
		private async void SearchGuests(Task obj)
		{
			if (filteredText?.Length > 3)
			{
				// Call your search function with searchText
				await GetSearchedGuests(filteredText);
			}
			else if (filteredText?.Length == 0)
			{
				// If searchText is empty, call function to get all guests
				await GetGuestsFromDb();
			}
			await InvokeAsync(StateHasChanged);
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
		private async Task GetSearchedGuests(string? filteredText)
		{
			var pagedGuests = await _guestService.SearchGuests(filteredText, _pagingParameters);
			indexes = new List<int>();
			guests = pagedGuests.Items.ToList();
			_pagingParameters.TotalPages = pagedGuests.TotalPages;
			for (int i = 1; i < guests.Count() + 1; i++)
				indexes.Add(pagedGuests.PageSize * (pagedGuests.PageNumber - 1) + i);
		}
		public void Dispose()
		{
			src.Dispose();
		}

	}
}
