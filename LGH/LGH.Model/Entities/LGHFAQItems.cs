using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGH.Model.Entities
{
   public class LGHFAQItems
    {

        public long ItemId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? Modified { get; set; }
    }
}
