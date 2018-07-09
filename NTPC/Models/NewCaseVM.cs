using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTPC.Models
{
    public class NewCaseVM
    {
        public bool IsEditMode { get; set; }
        public long CallId { get; set; }
        // public DateTime CallTime { get; set; }
        public string CallTime { get; set; }

        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleLocation { get; set; }

        public SelectList CallTypes { get; set; }

        public SelectList ChiefComplaintTypes { get; set; }
        public SelectList StandardRemarks { get; set; }

        public int SelectedCallType { get; set; }
        public int SelectedComplaintType { get; set; }
        public int SelectedRemark { get; set; }
        public int SelectedGender { get; set; }

        public Validation ValidationValues { get; set; }
        public CaseDetails CaseDetails { get; set; }
    }
}