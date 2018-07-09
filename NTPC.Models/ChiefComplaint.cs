using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class ChiefComplaint
    {
        public int ChiefComplaintId { get; set; }
        public string Complaint { get; set; }
        public bool IsValid { get; set; }

    }
}
