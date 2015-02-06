using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGH.Model.Entities
{
    public class LGHExchangeItems
    {
        public long ItemId { get; set; }
        public string ExchangeEventTitle { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? Time1 { get; set; }
        public DateTime? Time2 { get; set; }
        public DateTime? Time3 { get; set; }
        public string ExchangeMailbox { get; set; }
        public string EventLocation { get; set; }
        public string MeetingRoom { get; set; }
    }
}
