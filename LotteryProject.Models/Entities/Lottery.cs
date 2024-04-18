using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Entities
{
    public class Lottery : BaseEntity
    {
        public Guid GuestID { get; set; }
        public Guid PresentID { get; set; }

        public Guest? Guest { get; set; } = null;

        public Present? Present { get; set; } = null;
        public DateTime? LotteryDate { get; set; }
    }
}
