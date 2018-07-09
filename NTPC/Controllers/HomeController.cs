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
    public class HomeController : Controller
    {
        IUserDetails objIUserDetails = new UserRepository();
        public ActionResult Index()
        {
            var agentId = 1;
            List<VehicleVM> vehiclesList = new List<VehicleVM>();
            UserDetailViewModel model = new UserDetailViewModel();
            var vehicles = objIUserDetails.GetUserStateRelatedVehicles(1);
            vehicles.ForEach(x => vehiclesList.Add(new VehicleVM
            {
                VehicleId = x.VehicleId,
                VehicleNumber = x.VehicleNumber + " - " + x.State
            }));
            model.Vehicles = new SelectList(vehiclesList, "VehicleId", "VehicleNumber");

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}