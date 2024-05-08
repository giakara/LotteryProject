using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LotteryProject.Components
{
	public partial class LotteryListItem : ComponentBase
	{
		[Inject] private ILotteryService _lotteryService { get; set; } = null!;
		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;

		[Parameter]
		public Lottery LotteryItem { get; set; } = null!;

		[Parameter]
		public EventCallback<Lottery> OnLotteryDeleted { get; set; }
		[Parameter]
		public int Index { get; set; }
		public bool _isCommentSectionVisible { get; set; }

		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
		}
		protected override Task OnAfterRenderAsync(bool firstRender)
		{
			return base.OnAfterRenderAsync(firstRender);
		}

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
		}
		//protected override async Task OnParametersSetAsync()
		//{
		//	base.OnParametersSetAsync();
		//}
		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		//protected override async Task OnInitializedAsync()
		//{
		//	LotteryItem.Present = await _presentService.GetPresent(LotteryItem.PresentId);
		//	LotteryItem.Guest = await _guestService.GetGuest(LotteryItem.GuestId);
		//}



		private bool CommentSectionIsVisible()
		{
			return _isCommentSectionVisible;
		}


		protected async void DeleteRowItemFromList(Guid lotteryID)
		{
			await _lotteryService.DeleteLotteryById(lotteryID);

			if (LotteryItem is not null)
			{
				await OnLotteryDeleted.InvokeAsync(LotteryItem);
			}
		}
	}
}
