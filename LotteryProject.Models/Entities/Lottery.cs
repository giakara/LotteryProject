

namespace LotteryProject.Models.Entities
{
	public class Lottery(Guid presentId) : BaseEntity
	{
		public Guid GuestId { get; set; }
		public Guid PresentId { get; set; } = presentId;

		public Guest? Guest { get; set; } = null;

		public Present Present { get; set; } = null!;
		public DateTime? LotteryDate { get; set; }
	}
}
