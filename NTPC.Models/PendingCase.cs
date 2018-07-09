using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class PendingCase
    {
        public string AgentId { get; set; }
        public long CallId { get; set; }
        public DateTime CallTime { get; set; }
        public string CallerName { get; set; }
        public string ContactNumber { get; set; }
    }
}
