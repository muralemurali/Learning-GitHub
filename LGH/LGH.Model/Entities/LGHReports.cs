using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGH.Model.Entities
{
   public class LGHReports
    {

        public long ItemId { get; set; }
        public string Url { get; set; }
        public string shortNotes { get; set; }
        public DateTime? Modified { get; set; }
    }
}
