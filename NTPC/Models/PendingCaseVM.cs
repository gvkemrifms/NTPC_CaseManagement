using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTPC.Models
{
    public class PendingCaseVM
    {
        public string AgentId { get; set; }
        public long CallId { get; set; }
        public DateTime CallTime { get; set; }
        public string CallerName { get; set; }
        public string ContactNumber { get; set; }

        public string VehicleNumber { get; set; }
        public List<PendingCase> PendingCases { get; set; }
    }
}