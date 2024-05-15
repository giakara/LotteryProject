using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LotteryProject.Components
{
	public partial class LotteryListItem : ComponentBase
	{
		[Inject] private ILotteryService _lotteryService { get; set; } = null!;
		[Parameter]
		public Lottery LotteryItem { get; set; } = null!;

		[Parameter]
		public EventCallback<Lottery> OnLotteryDeleted { get; set; }
		[Parameter]
		public int Index { get; set; }
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
