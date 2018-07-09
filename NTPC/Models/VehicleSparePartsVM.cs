using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTPC.Models
{
    public class VehicleSparePartsVM
    {
        public SelectList Manufacturers { get; set; }
        public int SelectedManufacturer { get; set; }

        public string SparePartName { get; set; }
        public int ManufacturerId { get; set; }
        public int Cost { get; set; }
    }
}