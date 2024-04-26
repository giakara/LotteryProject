using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Validations
{
	public interface IGuestValidationProperties
	{
		public Guid Id { get; set; }
		public string GuestName { get; set; }
		public string GuestSurname { get; set; }

		public bool IsDuplicated { get; set; }
	}
}
