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
	public partial class PresentList : ComponentBase
	{

		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject]
		protected NavigationManager NavigationManager { get; set; }
		private IEnumerable<Present> presents { get; set; } = null!;
		private string? filteredText;

		private List<int> indexes { get; set; } = new List<int>();
		private CancellationTokenSource src = new();
		private PagingParameters _pagingParameters = new PagingParameters() { PageSize = 5 }; //appsettings


		protected override async Task OnInitializedAsync()
		{
			await Task.Delay(500);
			await GetPresentsFromDb();
		}
		private async Task GetPresentsFromDb()
		{
			var pagedPresents = await _presentService.GetPaged(_pagingParameters);
			indexes = new List<int>();
			presents = pagedPresents.Items.ToList();
			_pagingParameters.TotalPages = pagedPresents.TotalPages;
			for (int i = 1; i < presents.Count() + 1; i++)
				indexes.Add(pagedPresents.PageSize * (pagedPresents.PageNumber - 1) + i);
		}
		private async Task OnChangeTask(ChangeEventArgs args)
		{
			filteredText = args.Value.ToString();
			src.Cancel();
			src = new();
			await Task.Delay(500, src.Token).ContinueWith(SearchPresents, src.Token);

		}
		private async void SearchPresents(Task obj)
		{
			if (filteredText?.Length > 3)
			{
				await GetSearchedPresents(filteredText);
			}
			else if (filteredText?.Length == 0)
			{
				await GetPresentsFromDb();
			}
			await InvokeAsync(StateHasChanged);
		}
		private async Task HandlePresentDeleted(Present present)
		{
			await GetPresentsFromDb();
		}
		protected void AddPresent()
		{
			NavigationManager.NavigateTo("/AddPresent");
		}
		private async Task SelectedPage(int page)
		{
			_pagingParameters.PageNumber = page;
			await GetPresentsFromDb();
		}
		private async Task SearchPresent(string? filteredText)
		{
			if (filteredText?.Length > 3)
			{
				await GetSearchedPresents(filteredText);
			}
			else if (filteredText?.Length == 0)
			{
				await GetPresentsFromDb();
			}
		}
		private async Task GetSearchedPresents(string? filteredText)
		{
			var pagedPresents = await _presentService.SearchPresents(filteredText, _pagingParameters);
			indexes = new List<int>();
			presents = pagedPresents.Items.ToList();
			_pagingParameters.TotalPages = pagedPresents.TotalPages;
			for (int i = 1; i < presents.Count() + 1; i++)
				indexes.Add(pagedPresents.PageSize * (pagedPresents.PageNumber - 1) + i);
		}
		public void Dispose()
		{
			src.Dispose();
		}

	}
}
