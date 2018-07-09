using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class CallIncidentInfo
    {
        public long CallId { get; set; }
        public DateTime CallStartTime { get; set; }
        public string AgentId { get; set; }
        public string ServiceId { get; set; }
        public string PhoneNo { get; set; }
        public string PopUpTime { get; set; }
        public DateTime CallEndTime { get; set; }
    }
}
