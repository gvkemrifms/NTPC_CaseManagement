using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTPC.Models
{
    public class UserDetailViewModel
    {
        public Login LoginDetails { get; set; }
        public SelectList Vehicles { get; set; }

        public int SelectedVehicle { get; set; }
        public int vehicleNumberSelected { get; set; }

    }
}