
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace LotteryProject.Models.Entities
{
    public class Guest : BaseEntity
    {
        public string? GuestName { get; set; }
        public string? GuestSurname { get; set; }
        public virtual Lottery? Lottery { get; set; } = null;
    }

}
