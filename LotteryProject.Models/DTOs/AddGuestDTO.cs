
namespace LotteryProject.Models.DTOs
{
    public class AddGuestDTO(string guestName, string guestSurname)
    {
        public string GuestName { get; set; } = guestName;
        public string GuestSurname { get; set; } = guestSurname;
    }
}
