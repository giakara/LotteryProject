using FluentValidation;
using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Client.Shared.ViewModels;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Components
{
	public partial class GuestItemForm : ComponentBase
	{
		[Inject] private IGuestService _guestService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;
		[Inject] protected IValidator<GuestViewModel> Validator { get; set; } = null!;

		//private EditContext _editContext { get; set; } = null!;
		public GuestViewModel ItemViewModel { get; set; }

		public IEnumerable<Guest> Guests { get; set; }

		//private ValidationMessageStore _messageStore = null!;
		[Parameter] public string guestID { get; set; } = string.Empty;


		protected override async Task OnInitializedAsync()
		{
			ItemViewModel = new();


			if (!string.IsNullOrEmpty(guestID))
			{
				var guestIDGuid = new Guid(guestID);
				Guest guest = await _guestService.GetGuest(guestIDGuid);

				if (guest is not null)
				{
					ItemViewModel.GuestSurname = guest.GuestSurname;
					ItemViewModel.GuestName = guest.GuestName;
					ItemViewModel.Id = guest.Id;
				}
			}
			Guests = await _guestService.GetAllGuests();

		}
		protected async Task HandleValidSubmit()
		{
			//Add
			if (string.IsNullOrEmpty(guestID))
			{
				var newGuest = new AddGuestDTO(ItemViewModel.GuestName, ItemViewModel.GuestSurname);

				ItemViewModel.IsDuplicated = Guests.Any(x => x.GuestName == newGuest.GuestName && x.GuestSurname == newGuest.GuestSurname);

				var valres = await Validator.ValidateAsync(ItemViewModel, opt => opt.IncludeRuleSets("GuestItemDetails"));

				if (valres.IsValid)
				{
					var isItemAdded = await _guestService.AddGuest(newGuest);

					if (isItemAdded is not null)
					{
						_navigationManager.NavigateTo("/GuestList");
					}
				}
				else
				{

					await InvokeAsync(StateHasChanged);
					valres.Errors.Clear();
				}
			}
			else //Update
			{
				var updateGuest = new Guest(ItemViewModel.GuestName, ItemViewModel.GuestSurname)
				{
					Id = new Guid(guestID)
				};

				var valres = Validator.Validate(ItemViewModel, opt => opt.IncludeRuleSets("GuestItemDetails"));

				if (valres.IsValid)
				{
					var isItemUpdated = await _guestService.UpdateGuest(updateGuest);

					if (isItemUpdated is not null)
					{
						_navigationManager.NavigateTo("/GuestList");
					}
				}
				else
				{
					await InvokeAsync(StateHasChanged);
				}

			}
		}

		private void BackToList()
		{
			_navigationManager.NavigateTo("/GuestList");
		}
	}
}
