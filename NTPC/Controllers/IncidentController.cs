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
    public class IncidentController : Controller
    {
        ICallIncident callIncident = new IncidentRepository();
        // GET: Incident

        public enum gender
        {
            Male = 1,
            Female = 2,
            NA = 3
        }
        public ActionResult CreateNewCase(int vehicleId, string vehicleNumber)
        {
            Login log = new Login();
            NewCaseVM newCaseVM = new NewCaseVM();
            var newCase = callIncident.GetCallSequenceId("Pranay");
            if (vehicleNumber == null)
            {
                vehicleNumber = "AP10TM7172-Telangana";
            }

            newCaseVM.CallId = newCase.CallId;
            newCaseVM.CallTime = newCase.CallStartTime != null ? newCase.CallStartTime.ToString() : DateTime.Now.ToString();
            newCaseVM.VehicleId = vehicleId;
            newCaseVM.VehicleNumber = vehicleNumber.Split('-')[0];
            newCaseVM.VehicleLocation = vehicleNumber.Split('-')[1];
            newCaseVM.IsEditMode = false;
            newCaseVM.CaseDetails = new CaseDetails();
            newCaseVM.ValidationValues = new Validation
            {
                BaseReachOdoInstituteDepartureDiff = 100,
                InstituteDepartureTimeInstituteReachDiff = 120,
                InstituteReachOdoSceneReachOdoDiff = 100,
                InstituteReachTimeSceneDepartureDiff = 120,
                SceneDepartureTimeSceneReachDiff = 120,
                SceneReachOdoStartOdoDiff = 100,
                StartTimeSceneReachDiff = 120
            };

            GetCallTypes(newCaseVM);
            GetChiefComplaints(newCaseVM);
            GetStandardRemarks(newCaseVM);

            return View("CreateNewCase", newCaseVM);
        }

        [HttpPost]
        public JsonResult SaveCase(CaseDetailsVM caseDetails)
        {
            List<VictimDetails> victims = new List<VictimDetails>();
            var agentId = "Pranay";
            foreach (var item in caseDetails.VictimsDetails)
            {
                VictimDetails victim = new VictimDetails
                {
                    Name = item.Name,
                    Age = item.Age,
                    ChiefComplaint = item.ChiefComplaint,
                    Gender = item.Gender,
                    Remarks = item.Remarks,
                    VictimId = item.VictimId
                };
                victims.Add(victim);
            }

            CaseDetails newCaseDetails = new CaseDetails
            {
                CallId = caseDetails.CallId,
                CallerName = caseDetails.CallerName,
                CallTime = caseDetails.CallTime,
                CallType = caseDetails.CallType,
                IncidentLocation = caseDetails.IncidentLocation,
                CaseStartTime = caseDetails.CaseStartTime,
                ContactNumber = caseDetails.ContactNumber,
                HospitalHandOverTime = caseDetails.HospitalHandOverTime,
                BackToBaseTime = caseDetails.BackToBaseTime,
                InstituteReachTime = caseDetails.InstituteReachTime,
                SceneReachTime = caseDetails.SceneReachTime,
                SceneDepartureTime = caseDetails.SceneDepartureTime,
                StandardRemark = caseDetails.StandardRemark,
                InstituteReachOdo = caseDetails.InstituteReachOdo,
                BackToBaseOdo = caseDetails.BackToBaseOdo,
                SceneReachOdo = caseDetails.SceneReachOdo,
                StartOdo = caseDetails.StartOdo,
                VehicleDetails = new Vehicle
                {
                    VehicleId = caseDetails.VehicleDetails.VehicleId,
                    VehicleNumber = caseDetails.VehicleDetails.VehicleNumber,
                    State = caseDetails.VehicleDetails.State
                },
                VictimsDetails = victims
            };
            var status = 2;
            var result = callIncident.SaveNewCaseDetails(newCaseDetails, agentId, status);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult CloseCase(CaseDetailsVM caseDetails)
        {
            List<VictimDetails> victims = new List<VictimDetails>();
            var agentId = "Pranay";
            foreach (var item in caseDetails.VictimsDetails)
            {
                VictimDetails victim = new VictimDetails
                {
                    Name = item.Name,
                    Age = item.Age,
                    ChiefComplaint = item.ChiefComplaint,
                    Gender = item.Gender,
                    Remarks = item.Remarks
                };
                victims.Add(victim);
            }

            CaseDetails newCaseDetails = new CaseDetails
            {
                CallId = caseDetails.CallId,
                CallerName = caseDetails.CallerName,
                CallTime = caseDetails.CallTime,
                CallType = caseDetails.CallType,
                IncidentLocation = caseDetails.IncidentLocation,
                CaseStartTime = caseDetails.CaseStartTime,
                ContactNumber = caseDetails.ContactNumber,
                HospitalHandOverTime = caseDetails.HospitalHandOverTime,
                BackToBaseTime = caseDetails.BackToBaseTime,
                InstituteReachTime = caseDetails.InstituteReachTime,
                SceneReachTime = caseDetails.SceneReachTime,
                SceneDepartureTime = caseDetails.SceneDepartureTime,
                StandardRemark = caseDetails.StandardRemark,
                InstituteReachOdo = caseDetails.InstituteReachOdo,
                BackToBaseOdo = caseDetails.BackToBaseOdo,
                SceneReachOdo = caseDetails.SceneReachOdo,
                StartOdo = caseDetails.StartOdo,
                VehicleDetails = new Vehicle
                {
                    VehicleId = caseDetails.VehicleDetails.VehicleId,
                    VehicleNumber = caseDetails.VehicleDetails.VehicleNumber,
                    State = caseDetails.VehicleDetails.State
                },
                VictimsDetails = victims
            };
            var status = 4;
            var result = callIncident.CloseCase(newCaseDetails, agentId, status);
            return Json(new { success = true });
        }

        public ActionResult PendingCases(string vehicleNumber)
        {
            PendingCaseVM pendingCases = new PendingCaseVM();
            var agentId = "pranay";
            pendingCases.VehicleNumber = vehicleNumber;
            pendingCases.PendingCases = callIncident.GetPendingCases(agentId, vehicleNumber);
            return View(pendingCases);
        }

        public ActionResult EditCaseIncident(string callId)
        {
            NewCaseVM editingCase = new NewCaseVM();
            editingCase.IsEditMode = true;
            var agentId = "Pranay";
            GetCallTypes(editingCase);
            GetChiefComplaints(editingCase);
            GetStandardRemarks(editingCase);
            var caseDetails = callIncident.GetCaseDetails(callId, agentId);
            editingCase.CallId = Convert.ToInt64(caseDetails.CallId);
            editingCase.CallTime = caseDetails.CallTime.ToString();
            editingCase.VehicleNumber = caseDetails.VehicleDetails.VehicleNumber;
            editingCase.SelectedCallType = caseDetails.CallType;
            editingCase.SelectedRemark = caseDetails.StandardRemark;
            editingCase.CaseDetails = caseDetails;
            editingCase.VehicleId = caseDetails.VehicleDetails.VehicleId;
            editingCase.VehicleLocation = caseDetails.VehicleDetails.State;
            editingCase.VehicleNumber = caseDetails.VehicleDetails.VehicleNumber;
            editingCase.ValidationValues = new Validation
            {
                BaseReachOdoInstituteDepartureDiff = 100,
                InstituteDepartureTimeInstituteReachDiff = 120,
                InstituteReachOdoSceneReachOdoDiff = 100,
                InstituteReachTimeSceneDepartureDiff = 120,
                SceneDepartureTimeSceneReachDiff = 120,
                SceneReachOdoStartOdoDiff = 100,
                StartTimeSceneReachDiff = 120
            };

            return View("CreateNewCase", editingCase);
        }

        private void GetChiefComplaints(NewCaseVM newCaseVM)
        {
            List<ChiefComplaintVM> chiefComplaintsVM = new List<ChiefComplaintVM>();
            var callTypes = callIncident.GetChiefComplaints();
            callTypes.ForEach(x => chiefComplaintsVM.Add(new ChiefComplaintVM
            {
                ChiefComplaintId = x.ChiefComplaintId,
                ChiefComplaint = x.Complaint
            }));
            newCaseVM.ChiefComplaintTypes = new SelectList(chiefComplaintsVM, "ChiefComplaintId", "ChiefComplaint");
        }

        private void GetCallTypes(NewCaseVM newCaseVM)
        {
            List<CallTypeVM> callTypesVM = new List<CallTypeVM>();
            var callTypes = callIncident.GetCallTypes();
            callTypes.ForEach(x => callTypesVM.Add(new CallTypeVM
            {
                CallTypeId = x.CallTypeId,
                CallTypeName = x.CallTypeName
            }));
            newCaseVM.CallTypes = new SelectList(callTypesVM, "CallTypeId", "CallTypeName");
        }

        private void GetStandardRemarks(NewCaseVM newCaseVM)
        {
            List<StandardRemarkVM> standardRemarksVM = new List<StandardRemarkVM>();
            var standardRemark = callIncident.GetStandardRemarks();
            if (newCaseVM.IsEditMode)
            {
                standardRemark.ForEach(x => standardRemarksVM.Add(new StandardRemarkVM
                {
                    RemarkId = x.RemarkId.ToString(),
                    RemarkName = x.RemarkName,
                    IsDestinationRequired = x.IsDestinationRequired
                }));
            }
            else
            {
                standardRemark.ForEach(x => standardRemarksVM.Add(new StandardRemarkVM
                {
                    RemarkId = x.RemarkId.ToString() + "-" + x.IsDestinationRequired,
                    RemarkName = x.RemarkName,
                    IsDestinationRequired = x.IsDestinationRequired
                }));
            }
            newCaseVM.StandardRemarks = new SelectList(standardRemarksVM, "RemarkId", "RemarkName");
        }

    }
}