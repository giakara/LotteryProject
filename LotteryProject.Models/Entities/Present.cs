using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.Models.Entities
{
    public class Present : BaseEntity
    {

        public string? Description { get; set; }
        public string? Category { get; set; }
        public virtual Lottery? Lottery { get; set; } = null;
    }
}
