using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class Validation
    {
        public int StartTimeSceneReachDiff { get; set; }
        public int SceneDepartureTimeSceneReachDiff { get; set; }
        public int InstituteReachTimeSceneDepartureDiff { get; set; }
        public int InstituteDepartureTimeInstituteReachDiff { get; set; }

        public int SceneReachOdoStartOdoDiff { get; set; }
        public int InstituteReachOdoSceneReachOdoDiff { get; set; }
        public int BaseReachOdoInstituteDepartureDiff { get; set; }
    }
}
