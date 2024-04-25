//using FluentValidation;
//using LotteryProject.Client.Shared.Services.Interfaces;
//using Microsoft.AspNetCore.Components;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LotteryProject.Components
//{
//	public partial class GuestListForm : ComponentBase
//	{
//		[Inject] private IGuestService _guestService { get; set; } = null!;
//		[Inject] private NavigationManager _navigationManager { get; set; } = null!;
//		[Inject] protected IValidator<GuestViewModel> Validator { get; set; } = null!;

//		//private EditContext _editContext { get; set; } = null!;
//		public TodoItemViewModel ItemViewModel { get; set; }

//		//private ValidationMessageStore _messageStore = null!;
//		[Parameter] public string toDoItemId { get; set; } = string.Empty;


//		protected override async Task OnInitializedAsync()
//		{
//			ItemViewModel = new();
//			//_editContext = new EditContext(ItemViewModel);
//			//_messageStore = new ValidationMessageStore(_editContext);

//			if (!string.IsNullOrEmpty(toDoItemId))
//			{
//				var todoItemIdGuid = new Guid(toDoItemId);
//				TodoItemDtoResponse? todoItem = await _todoItemsService.GetById(todoItemIdGuid);

//				if (todoItem is not null)
//				{
//					ItemViewModel.Title = todoItem.Title;
//					ItemViewModel.Description = todoItem.Description;
//					ItemViewModel.Id = todoItem.Id;
//				}
//			}
//		}
//		protected async Task HandleValidSubmit()
//		{
//			//Add
//			if (string.IsNullOrEmpty(toDoItemId))
//			{
//				var newTodoItem = new CreateTodoItemDto(ItemViewModel.Title, ItemViewModel.Description);
//				//_messageStore.Clear();

//				var valres = Validator.Validate(ItemViewModel, opt => opt.IncludeRuleSets("TodoItemDetails"));

//				if (valres.IsValid)
//				{
//					var isItemAdded = await _todoItemsService.Add(newTodoItem);

//					if (isItemAdded is not null)
//					{
//						_navigationManager.NavigateTo("/tododashboard");
//					}
//				}
//				else
//				{
//					//foreach (var failure in valres.Errors)
//					//{
//					//    _messageStore.Add(_editContext.Field(failure.PropertyName), failure.ErrorMessage);
//					//}
//					StateHasChanged();
//				}
//			}
//			else //Update
//			{
//				var newUpdateTodoItem = new UpdateTodoItemDto(new Guid(toDoItemId), ItemViewModel.Title, ItemViewModel.Description);
//				//_messageStore.Clear();

//				var valres = Validator.Validate(ItemViewModel, opt => opt.IncludeRuleSets("TodoItemDetails"));

//				if (valres.IsValid)
//				{
//					var isItemUpdated = await _todoItemsService.Update(newUpdateTodoItem);

//					if (isItemUpdated is not null)
//					{
//						_navigationManager.NavigateTo("/tododashboard");
//					}
//				}
//				else
//				{
//					foreach (var failure in valres.Errors)
//					{
//						//_messageStore.Add(_editContext.Field(failure.PropertyName), failure.ErrorMessage);
//					}
//					StateHasChanged();
//				}

//			}
//		}

//		private void BackToList()
//		{
//			_navigationManager.NavigateTo("/tododashboard");
//		}
//	}
//}
