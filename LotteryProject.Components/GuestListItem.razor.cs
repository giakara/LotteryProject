using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LotteryProject.Components
{
	public partial class GuestListItem : ComponentBase
	{
		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;

		[Parameter]
		public Guest GuestItem { get; set; } = null!;

		[Parameter]
		public EventCallback<Guest> OnGuestDeleted { get; set; }
		[Parameter]
		public int Index { get; set; }


		private void NavigateToEdit(Guid guestID)
		{
			_navigationManager.NavigateTo($"/EditGuest/{guestID}");
		}

		protected async void DeleteRowItemFromList(Guid guestID)
		{
			await _guestService.DeleteGuestById(guestID);

			if (GuestItem is not null)
			{
				await OnGuestDeleted.InvokeAsync(GuestItem);
			}
		}
	}
}
