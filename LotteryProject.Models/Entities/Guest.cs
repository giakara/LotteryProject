
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LotteryProject.Models.Entities
{
	public class Guest(string guestName, string guestSurname, string email) : BaseEntity
	{
		public string GuestName { get; set; } = guestName;
		public string GuestSurname { get; set; } = guestSurname;
		public string Email { get; set; } = email;
		public ICollection<Lottery> Lotteries { get; } = [];
	}

}
