
using System.Text.Json.Serialization;

namespace LotteryProject.Models.Entities
{
	public class Lottery : BaseEntity
	{
		public Guid GuestId { get; set; }
		public Guid PresentId { get; set; }
		public Guest Guest { get; set; }
		public Present Present { get; set; }
		public DateTime LotteryDate { get; set; }

		// Parameterless constructor required by Entity Framework Core
		public Lottery()
		{
		}

		// Constructor with parameters
		public Lottery(Guid presentId, Guid guestId, DateTime lotteryDate)
		{
			PresentId = presentId;
			GuestId = guestId;
			LotteryDate = lotteryDate;
		}
	}
}