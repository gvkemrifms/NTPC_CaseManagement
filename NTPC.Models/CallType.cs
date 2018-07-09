using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class CallType
    {
        public int CallTypeId { get; set; }
        public string CallTypeName { get; set; }
        public bool IsValid { get; set; }
    }
}
