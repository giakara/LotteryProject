using LotteryProject.Client.Shared.Helpers;
using LotteryProject.Models.Paging;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Components
{
	public partial class Pager : ComponentBase
	{
		[Parameter]
		public PagingParameters PagingParameters { get; set; } = null!;

		public int Spread { get; set; } = 2;

		[Parameter]
		public EventCallback<int> SelectedPage { get; set; }

		private List<PagingLink> _links = null!;

		protected override Task OnAfterRenderAsync(bool firstRender)
		{
			return base.OnAfterRenderAsync(firstRender);
		}
		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}
		protected override void OnParametersSet()
		{
			CreatePaginationLinks();
		}

		private void CreatePaginationLinks()
		{
			_links = [new PagingLink(PagingParameters.PageNumber - 1, PagingParameters.PageNumber - 1 > 0, "Previous")];

			for (int i = 1; i <= PagingParameters.TotalPages; i++)
			{
				if (i > PagingParameters.PageNumber - Spread && i <= PagingParameters.PageNumber + Spread - 1)
				{
					_links.Add(new PagingLink(i, true, i.ToString()) { IsCurrent = PagingParameters.PageNumber == i });
				}
			}
			_links.Add(new PagingLink(PagingParameters.PageNumber + 1, PagingParameters.TotalPages - PagingParameters.PageNumber - 1 > 0, "Next"));
		}
		private async Task OnSelectedPage(PagingLink link)
		{
			if (link.Page == PagingParameters.PageNumber || !link.Enabled)
				return;

			await SelectedPage.InvokeAsync(link.Page);
		}
	}
}

