﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Validations
{
	public interface IPresentValidationProperties
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }

	}
}
