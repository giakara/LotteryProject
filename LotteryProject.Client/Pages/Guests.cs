using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.CompilerServices;

namespace LotteryProject.Client.Pages
{
	public partial class Guests
	{
		[Inject]
		private IGuestService _guestService { get; set; } = null!;

		public IEnumerable<Guest> guests { get; set; } = new List<Guest>();
		protected override async Task OnInitializedAsync()
		{
			var guestsFromDb = await _guestService.GetAllGuests();
			if (guestsFromDb?.Count() > 0)
			{
				guests = guestsFromDb;
			}
		}
		//private async Task GetGuestsFromDatabase()
		//{

		//}
	}
}
