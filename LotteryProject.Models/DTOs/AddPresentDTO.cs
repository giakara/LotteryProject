
namespace LotteryProject.Models.DTOs
{
    public class AddPresentDTO(string description)
    {
        public string Description { get; set; } = description;
        public string? Category { get; set; }
    }
}
