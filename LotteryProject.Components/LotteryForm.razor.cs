
using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;


namespace LotteryProject.Components
{
	public partial class LotteryForm : ComponentBase
	{
		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject] private ILotteryService _lotteryService { get; set; } = null!;
		[Inject] private IEmailService _emailService { get; set; } = null!;
		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;
		//[Inject] protected IValidator<PresentViewModel> Validator { get; set; } = null!;

		//private EditContext _editContext { get; set; } = null!;

		public IEnumerable<Present> Presents { get; set; }
		private RadzenDropDown<Guid> radzenDropDown;
		bool isOpened = false;


		//private ValidationMessageStore _messageStore = null!;
		[Parameter] public string lotteryID { get; set; } = string.Empty;
		public string lotteryWinner { get; set; } = string.Empty;
		public Guid selectedSearchValue { get; set; }
		private IEnumerable<Present> allPresents { get; set; } = null!;
		public string selectedAutoCompleteText { get; set; }
		protected override async Task OnInitializedAsync()
		{
			var lotteries = await _lotteryService.GetAllLotteries();
			allPresents = await _presentService.GetAllPresents();
			Presents = allPresents.Where(present => !lotteries.Any(lottery => lottery.PresentId == present.Id)).ToList();
		}
		private void OpenModal()
		{
			isOpened = true;
		}
		protected async Task NewLottery()
		{
			if (isOpened || selectedSearchValue == Guid.Empty) return;

			Guid presentID = allPresents.First(x => x.Id == selectedSearchValue).Id;

			var lotteryDTO = new AddEditLotteryDTO(presentID);

			var lottery = await _lotteryService.CreateLottery(lotteryDTO);
			if (lottery != null)
			{
				var guest = await _guestService.GetGuest(lottery.GuestId);
				lotteryWinner = guest.GuestSurname + " " + guest.GuestName;
				await _emailService.SendEmail(new EmailData
				{ EmailAddress = guest.Email, Subject = "Gongatulations", Message = "You win a " + lottery.Present.Description });
				OpenModal();
			}
		}
		private void BackToList()
		{
			_navigationManager.NavigateTo("/LotteryList");
		}
	}
}
