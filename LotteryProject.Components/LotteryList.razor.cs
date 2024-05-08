using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Components;

namespace LotteryProject.Components
{
	public partial class LotteryList : ComponentBase
	{

		[Inject] private ILotteryService _lotteryService { get; set; } = null!;
		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
		private IEnumerable<Lottery> lotteries { get; set; } = null!;

		private List<int> indexes { get; set; } = new List<int>();
		private CancellationTokenSource src = new();
		private PagingParameters _pagingParameters = new PagingParameters() { PageSize = 5 }; //appsettings


		protected override async Task OnInitializedAsync()
		{
			await Task.Delay(500);
			await GetLotteriesFromDb();
		}
		private async Task GetLotteriesFromDb()
		{
			var pagedLotteries = await _lotteryService.GetPaged(_pagingParameters);
			indexes = new List<int>();
			lotteries = pagedLotteries.Items.ToList();
			_pagingParameters.TotalPages = pagedLotteries.TotalPages;
			for (int i = 1; i < lotteries.Count() + 1; i++)
				indexes.Add(pagedLotteries.PageSize * (pagedLotteries.PageNumber - 1) + i);
		}

		private async Task HandleLotteryDeleted(Lottery lottery)
		{
			await GetLotteriesFromDb();
		}
		protected void CreateLottery()
		{
			NavigationManager.NavigateTo("/CreateLottery");
		}
		private async Task SelectedPage(int page)
		{
			_pagingParameters.PageNumber = page;
			await GetLotteriesFromDb();
		}


	}
}
