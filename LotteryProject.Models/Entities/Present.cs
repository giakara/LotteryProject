

namespace LotteryProject.Models.Entities
{
    public class Present(string description) : BaseEntity
    {

        public string Description { get; set; } = description;
        public string? Category { get; set; }
    }
}
