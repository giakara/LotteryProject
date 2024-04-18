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

        public Guest? Guest { get; set; }

        public Present? Present { get; set; }
        public DateTime? LotteryDate { get; set; }
    }
}
