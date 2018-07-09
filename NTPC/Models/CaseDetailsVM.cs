using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTPC.Models
{
    public class CaseDetailsVM
    {
        public string CallId { get; set; }
        public DateTime CallTime { get; set; }
        public int CallType { get; set; }
        public string ContactNumber { get; set; }
        public string CallerName { get; set; }
        public string IncidentLocation { get; set; }
        public bool IsDestinationRequired { get; set; }

        public IEnumerable<VictimDetails> VictimsDetails { get; set; }

        public Vehicle VehicleDetails { get; set; }

        public int StandardRemark { get; set; }
        public DateTime CaseStartTime { get; set; }
        public DateTime SceneReachTime { get; set; }
        public DateTime SceneDepartureTime { get; set; }
        public DateTime InstituteReachTime { get; set; }
        public DateTime HospitalHandOverTime { get; set; }
        public DateTime BackToBaseTime { get; set; }

        public string StartOdo { get; set; }
        public string SceneReachOdo { get; set; }
        public string InstituteReachOdo { get; set; }
        public string BackToBaseOdo { get; set; }
    }
}