using NTPC.DAL;
using NTPC.Models;
using NTPC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTPC.Controllers
{
    public class VehicleController : Controller
    {
        IVehicleDetails vehDetails = new VehicleRepository();
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SpareParts()
        {
            List<ManufacturerVM> manufacturersList = new List<ManufacturerVM>();

            VehicleSparePartsVM model = new VehicleSparePartsVM();
            var manufactures = vehDetails.GetManufacturers();

            manufactures.ForEach(x => manufacturersList.Add(new ManufacturerVM
            {
                ManufacturerId = x.ManufacturerId,
                ManufacturerName = x.ManufacturerName
            }));

            model.Manufacturers = new SelectList(manufacturersList, "ManufacturerId", "ManufacturerName");
            return View(model);
        }

        public JsonResult SaveSparePartAndManufacturer(VehicleSparePartsVM saveSpareAndManufacturerDetails)
        {
            var success = vehDetails.SaveSparePartDetails(saveSpareAndManufacturerDetails.ManufacturerId,
                saveSpareAndManufacturerDetails.SparePartName, saveSpareAndManufacturerDetails.Cost);
            return Json(success);
        }
    }
}