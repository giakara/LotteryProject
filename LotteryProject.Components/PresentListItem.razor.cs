using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LotteryProject.Components
{
	public partial class PresentListItem : ComponentBase
	{
		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;

		[Parameter]
		public Present PresentItem { get; set; } = null!;

		[Parameter]
		public EventCallback<Present> OnPresentDeleted { get; set; }
		[Parameter]
		public int Index { get; set; }

		private void NavigateToEdit(Guid presentID)
		{
			_navigationManager.NavigateTo($"/EditPresent/{presentID}");
		}

		protected async void DeleteRowItemFromList(Guid presentID)
		{
			await _presentService.DeletePresentById(presentID);

			if (PresentItem is not null)
			{
				await OnPresentDeleted.InvokeAsync(PresentItem);
			}
		}
	}
}
