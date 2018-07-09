using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class StandardRemark
    {
        public int RemarkId { get; set; }
        public string RemarkName { get; set; }
        public bool IsDestinationRequired { get; set; }
    }
}
