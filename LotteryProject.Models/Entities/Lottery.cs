

namespace LotteryProject.Models.Entities
{
	public class Lottery(Guid presentID) : BaseEntity
	{
		public Guid GuestID { get; set; }
		public Guid PresentID { get; set; } = presentID;

		public Guest? Guest { get; set; } = null;

		public Present Present { get; set; } = null!;
		public DateTime? LotteryDate { get; set; }
	}
}
