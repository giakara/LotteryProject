using LotteryProject.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.ViewModels
{
	public class GuestViewModel : IGuestValidationProperties
	{
		public string GuestName { get; set; } = null!;
		public string GuestSurname { get; set; } = null!;
		public Guid Id { get; set; }
		public bool IsDuplicated { get; set; }
	}
}
