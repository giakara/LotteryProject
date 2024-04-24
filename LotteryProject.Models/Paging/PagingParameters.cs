using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Paging
{
	public class PagingParameters
	{
		public int maxPageSize { get; set; } = 50;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = 10;
		public int TotalPages { get; set; }
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > maxPageSize ? maxPageSize : value; }
		}
	}
}
