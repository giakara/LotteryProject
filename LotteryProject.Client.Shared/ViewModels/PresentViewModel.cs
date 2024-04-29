using LotteryProject.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.ViewModels
{
	public class PresentViewModel : IPresentValidationProperties
	{
		public string Description { get; set; } = null!;
		public string Category { get; set; } = null!;
		public Guid Id { get; set; }
	}
}
