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
	public partial class PresentItemForm : ComponentBase
	{
		[Inject] private IPresentService _presentService { get; set; } = null!;
		[Inject] private NavigationManager _navigationManager { get; set; } = null!;
		[Inject] protected IValidator<PresentViewModel> Validator { get; set; } = null!;

		//private EditContext _editContext { get; set; } = null!;
		public PresentViewModel ItemViewModel { get; set; }

		public IEnumerable<Present> Presents { get; set; }

		//private ValidationMessageStore _messageStore = null!;
		[Parameter] public string presentID { get; set; } = string.Empty;


		protected override async Task OnInitializedAsync()
		{
			ItemViewModel = new();


			if (!string.IsNullOrEmpty(presentID))
			{
				var presentIDGuid = new Guid(presentID);
				Present present = await _presentService.GetPresent(presentIDGuid);

				if (present is not null)
				{
					ItemViewModel.Description = present.Description;
					ItemViewModel.Category = present.Category;
					ItemViewModel.Id = present.Id;
				}
			}
			Presents = await _presentService.GetAllPresents();

		}
		protected async Task HandleValidSubmit()
		{
			//Add
			if (string.IsNullOrEmpty(presentID))
			{
				var newPresent = new AddPresentDTO(ItemViewModel.Description, ItemViewModel.Category);


				var valres = await Validator.ValidateAsync(ItemViewModel, opt => opt.IncludeRuleSets("PresentItemDetails"));

				if (valres.IsValid)
				{
					var isItemAdded = await _presentService.AddPresent(newPresent);

					if (isItemAdded is not null)
					{
						_navigationManager.NavigateTo("/PresentList");
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
				var updatePresent = new Present(ItemViewModel.Description)
				{
					Id = new Guid(presentID),
					Category = ItemViewModel.Category,
				};

				var valres = Validator.Validate(ItemViewModel, opt => opt.IncludeRuleSets("PresentItemDetails"));

				if (valres.IsValid)
				{
					var isItemUpdated = await _presentService.UpdatePresent(updatePresent);

					if (isItemUpdated is not null)
					{
						_navigationManager.NavigateTo("/PresentList");
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
			_navigationManager.NavigateTo("/PresentList");
		}
	}
}
