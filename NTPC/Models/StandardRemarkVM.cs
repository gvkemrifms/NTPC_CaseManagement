using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTPC.Models
{
    public class StandardRemarkVM
    {
        public string RemarkId { get; set; }
        public string RemarkName { get; set; }
        public bool IsDestinationRequired { get; set; }
    }
}