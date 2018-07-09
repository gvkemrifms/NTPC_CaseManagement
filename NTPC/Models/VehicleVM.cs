using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTPC.Models
{
    public class VehicleVM
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string SelectedVehicleId { get; set; }
    }
}