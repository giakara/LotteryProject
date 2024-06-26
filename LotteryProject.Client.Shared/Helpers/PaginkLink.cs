﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.Helpers
{
	public class PagingLink
	{
		public string Text { get; set; }
		public int Page { get; set; }
		public bool Enabled { get; set; }
		public bool IsCurrent { get; set; }

		public PagingLink(int page, bool enabled, string text)
		{
			Page = page;
			Enabled = enabled;
			Text = text;
		}
	}
}
