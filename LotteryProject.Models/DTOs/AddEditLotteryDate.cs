using LotteryProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.DTOs
{
    public class AddEditLotteryDate
    {
        public Guid GuestID { get; set; }

        public Guid PresentID { get; set; }
    }
}
