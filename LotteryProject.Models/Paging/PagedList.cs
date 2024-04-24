using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Paging
{
	public class PagedList<T>
	{
		public IEnumerable<T> Items { get; set; } = null!;
		public int TotalCount { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalPages
		{
			get
			{
				if (TotalCount > 0 && PageSize > 0)
				{
					return (int)Math.Ceiling((double)TotalCount / PageSize);
				}
				return 0;
			}

		}
	}

}
