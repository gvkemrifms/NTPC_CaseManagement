using MySql.Data.MySqlClient;
using NTPC.DAL;
using NTPC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Repositories
{
    public class IncidentRepository : ICallIncident
    {
        public CallIncidentInfo GetCallSequenceId(string agentId)
        {
            CallIncidentInfo newCase = new CallIncidentInfo();
            try
            {
                MySqlConnection conn =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CreateNewCaseIncident";
                cmd.Parameters.AddWithValue("@agentid", agentId);

                ad.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    newCase.CallId = Convert.ToInt64(ds.Tables[0].Rows[0]["currentnumber"]);
                    newCase.CallStartTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["calltime"]);
                }
            }
            catch (Exception e)
            {

            }
            return newCase;
        }

        public List<CallType> GetCallTypes()
        {
            MySqlConnection Con = new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
            MySqlCommand Cmd = null;
            MySqlDataReader dr = null;
            using (Con)
            {
                Con.Open();
                Cmd = new MySqlCommand("SELECT * FROM m_call_type", Con);
                Cmd.CommandType = CommandType.Text;
                dr = Cmd.ExecuteReader();

                List<CallType> callTypes = new List<CallType>();
                while (dr.Read())
                {
                    CallType callType = new CallType();
                    callType.CallTypeId = Convert.ToInt16(dr["call_type_id"]);
                    callType.CallTypeName = dr["call_type_name"].ToString();
                    callType.IsValid = Convert.ToBoolean(dr["is_valid"]);

                    callTypes.Add(callType);
                }
                Con.Close();
                return callTypes;
            }
        }

        public List<ChiefComplaint> GetChiefComplaints()
        {
            MySqlConnection Con = new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
            MySqlCommand Cmd = null;
            MySqlDataReader dr = null;
            using (Con)
            {
                Con.Open();
                Cmd = new MySqlCommand("SELECT * FROM m_chief_complaint ORDER BY chief_complaint_name ASC", Con);
                Cmd.CommandType = CommandType.Text;
                dr = Cmd.ExecuteReader();

                List<ChiefComplaint> chiefComplaints = new List<ChiefComplaint>();
                while (dr.Read())
                {
                    ChiefComplaint complaint = new ChiefComplaint();
                    complaint.ChiefComplaintId = Convert.ToInt16(dr["chief_complaint_id"]);
                    complaint.Complaint = dr["chief_complaint_name"].ToString();
                    complaint.IsValid = Convert.ToBoolean(dr["is_active"]);

                    chiefComplaints.Add(complaint);
                }
                Con.Close();
                return chiefComplaints;
            }
        }

        public List<StandardRemark> GetStandardRemarks()
        {
            MySqlConnection Con = new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
            MySqlCommand Cmd = null;
            MySqlDataReader dr = null;
            using (Con)
            {
                Con.Open();
                Cmd = new MySqlCommand("SELECT * FROM m_standard_remarks ORDER BY standard_remarks_name ASC", Con);
                Cmd.CommandType = CommandType.Text;
                dr = Cmd.ExecuteReader();

                List<StandardRemark> standardRemarks = new List<StandardRemark>();
                while (dr.Read())
                {
                    StandardRemark remark = new StandardRemark();
                    remark.RemarkId = Convert.ToInt32(dr["standard_remarks_id"]);
                    remark.RemarkName = dr["standard_remarks_name"].ToString();
                    remark.IsDestinationRequired = Convert.ToBoolean(dr["isDestinationRequire"]);

                    standardRemarks.Add(remark);
                }
                Con.Close();
                return standardRemarks;
            }
        }

        public bool SaveNewCaseDetails(CaseDetails newCaseDetails, string agentId, int status)
        {
            var success = false;
            try
            {
                MySqlConnection conn =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "SaveNewCaseDetails";
                cmd.Parameters.AddWithValue("@agentid", agentId);
                cmd.Parameters.AddWithValue("@vCallid", newCaseDetails.CallId);
                cmd.Parameters.AddWithValue("@callerName", newCaseDetails.CallerName);
                cmd.Parameters.AddWithValue("@callType", newCaseDetails.CallType);
                cmd.Parameters.AddWithValue("@statusId", status);
                cmd.Parameters.AddWithValue("@phoneNumber", newCaseDetails.ContactNumber);
                cmd.Parameters.AddWithValue("@incidentLocation", newCaseDetails.IncidentLocation);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "SaveNewCaseVehicleAssignment";
                //cmd.Parameters.AddWithValue("@agentid", agentId);
                //cmd.Parameters.AddWithValue("@vCallid", newCaseDetails.CallId);
                cmd.Parameters.AddWithValue("@vehicleId", newCaseDetails.VehicleDetails.VehicleId);
                cmd.Parameters.AddWithValue("@startTime", newCaseDetails.CaseStartTime);
                cmd.Parameters.AddWithValue("@sceneReachTime", newCaseDetails.SceneReachTime);
                cmd.Parameters.AddWithValue("@sceneDepartureTime", newCaseDetails.SceneDepartureTime);
                cmd.Parameters.AddWithValue("@hospitalHandoverTime", newCaseDetails.HospitalHandOverTime);
                cmd.Parameters.AddWithValue("@instituteReachTime", newCaseDetails.InstituteReachTime);
                cmd.Parameters.AddWithValue("@backToBaseTime", newCaseDetails.BackToBaseTime);
                cmd.Parameters.AddWithValue("@vehicleNo", newCaseDetails.VehicleDetails.VehicleNumber);
                cmd.Parameters.AddWithValue("@startOdo", newCaseDetails.StartOdo);
                cmd.Parameters.AddWithValue("@sceneReachOdo", newCaseDetails.SceneReachOdo);
                cmd.Parameters.AddWithValue("@instituteReachOdo", newCaseDetails.InstituteReachOdo);
                cmd.Parameters.AddWithValue("@baseReachOdo", newCaseDetails.BackToBaseOdo);
                cmd.Parameters.AddWithValue("@callTime", newCaseDetails.CallTime);

                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@victimName", "");
                cmd.Parameters.AddWithValue("@victimAge", 0);
                cmd.Parameters.AddWithValue("@victimGender", 0);
                cmd.Parameters.AddWithValue("@victimRemarks", "");
                cmd.Parameters.AddWithValue("@chiefComplaintId", 0);
                cmd.Parameters.AddWithValue("@victimId", 0);
                cmd.Parameters.AddWithValue("@standardRemarksId", newCaseDetails.StandardRemark);

                foreach (var victim in newCaseDetails.VictimsDetails)
                {
                    cmd.CommandText = "SaveNewCaseVictimDetails";
                    cmd.Parameters["@victimName"].Value = victim.Name;
                    cmd.Parameters["@victimAge"].Value = victim.Age;
                    cmd.Parameters["@victimGender"].Value = victim.Gender;
                    cmd.Parameters["@victimRemarks"].Value = victim.Remarks;
                    cmd.Parameters["@chiefComplaintId"].Value = victim.ChiefComplaint;
                    cmd.Parameters["@victimId"].Value = victim.VictimId;

                    cmd.ExecuteNonQuery();
                    success = true;
                }

                conn.Close();
            }
            catch (Exception e)
            {

            }
            return success;
        }

        public List<PendingCase> GetPendingCases(string agentId, string vehicleNumber)
        {
            List<PendingCase> pendingCases = new List<PendingCase>();
            try
            {
                MySqlConnection conn =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetPendingCases";
                cmd.Parameters.AddWithValue("@agentid", agentId);
                cmd.Parameters.AddWithValue("@vehicleNumber", vehicleNumber.TrimEnd());

                ad.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PendingCase pendingCase = new PendingCase();
                        pendingCase.CallId = Convert.ToInt64(ds.Tables[0].Rows[i]["callid"]);
                        pendingCase.CallTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["entry_time"]);
                        pendingCase.CallerName = ds.Tables[0].Rows[i]["caller_name"].ToString();
                        pendingCase.ContactNumber = ds.Tables[0].Rows[i]["phone_number"].ToString();

                        pendingCases.Add(pendingCase);
                    }

                }
            }
            catch (Exception e)
            {

            }
            return pendingCases;
        }

        public CaseDetails GetCaseDetails(string callId, string agentId)
        {
            CaseDetails selectedCase = new CaseDetails();
            try
            {
                MySqlConnection conn =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetSelectedCase";
                cmd.Parameters.AddWithValue("@agentid", agentId);
                cmd.Parameters.AddWithValue("@vcallid", callId);

                ad.Fill(ds);
                conn.Close();
                List<VictimDetails> victims = new List<VictimDetails>();
                Vehicle vehicleDetails = new Vehicle();

                selectedCase.CallId = ds.Tables[0].Rows[0]["callid"].ToString();
                selectedCase.CallTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["entry_time"]);
                vehicleDetails.VehicleNumber = ds.Tables[0].Rows[0]["vehicle_no"].ToString();
                vehicleDetails.VehicleId = Convert.ToInt32(ds.Tables[0].Rows[0]["vehicle_id"]);
                selectedCase.VehicleDetails = vehicleDetails;

                selectedCase.CallType = Convert.ToInt32(ds.Tables[0].Rows[0]["call_type_id"]);
                selectedCase.ContactNumber = ds.Tables[0].Rows[0]["phone_number"].ToString();
                selectedCase.CallerName = ds.Tables[0].Rows[0]["caller_name"].ToString();
                selectedCase.IncidentLocation = ds.Tables[0].Rows[0]["locality"].ToString();

                selectedCase.StandardRemark = Convert.ToInt32(ds.Tables[0].Rows[0]["standard_remarks_id"]);
                selectedCase.CaseStartTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["start_time"]);
                selectedCase.SceneReachTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["scene_arrival_time"]);
                selectedCase.SceneDepartureTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["scene_depature_time"]);
                selectedCase.InstituteReachTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["institute_reach_time"]);
                selectedCase.HospitalHandOverTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["HandoverTime"]);
                selectedCase.BackToBaseTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BacktoBaseTime"]);
                selectedCase.StartOdo = ds.Tables[0].Rows[0]["odo_base_start"].ToString();
                selectedCase.SceneReachOdo = ds.Tables[0].Rows[0]["odo_scene"].ToString();
                selectedCase.InstituteReachOdo = ds.Tables[0].Rows[0]["odo_hospital"].ToString();
                selectedCase.BackToBaseOdo = ds.Tables[0].Rows[0]["odo_base_reach"].ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        VictimDetails victim = new VictimDetails();
                        victim.Name = ds.Tables[0].Rows[i]["victim_name"].ToString();
                        victim.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["age"]);
                        victim.Gender = ds.Tables[0].Rows[i]["gender"].ToString();
                        victim.Remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                        victim.ChiefComplaint = Convert.ToInt32(ds.Tables[0].Rows[i]["chief_complaint_id"]);
                        victim.VictimId = Convert.ToInt32(ds.Tables[0].Rows[i]["victim_id"]);

                        victims.Add(victim);
                    }
                }
                selectedCase.VictimsDetails = victims;
            }
            catch (Exception e)
            {

            }
            return selectedCase;
        }

        public bool CloseCase(CaseDetails newCaseDetails, string agentId, int status)
        {
            var success = false;
            try
            {
                MySqlConnection conn =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "SaveNewCaseDetails";
                cmd.Parameters.AddWithValue("@agentid", agentId);
                cmd.Parameters.AddWithValue("@vCallid", newCaseDetails.CallId);
                cmd.Parameters.AddWithValue("@callerName", newCaseDetails.CallerName);
                cmd.Parameters.AddWithValue("@callType", newCaseDetails.CallType);
                cmd.Parameters.AddWithValue("@statusId", status);
                cmd.Parameters.AddWithValue("@phoneNumber", newCaseDetails.ContactNumber);
                cmd.Parameters.AddWithValue("@incidentLocation", newCaseDetails.IncidentLocation);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "SaveNewCaseVehicleAssignment";
                //cmd.Parameters.AddWithValue("@agentid", agentId);
                //cmd.Parameters.AddWithValue("@vCallid", newCaseDetails.CallId);
                cmd.Parameters.AddWithValue("@vehicleId", newCaseDetails.VehicleDetails.VehicleId);
                cmd.Parameters.AddWithValue("@startTime", newCaseDetails.CaseStartTime);
                cmd.Parameters.AddWithValue("@sceneReachTime", newCaseDetails.SceneReachTime);
                cmd.Parameters.AddWithValue("@sceneDepartureTime", newCaseDetails.SceneDepartureTime);
                cmd.Parameters.AddWithValue("@hospitalHandoverTime", newCaseDetails.HospitalHandOverTime);
                cmd.Parameters.AddWithValue("@instituteReachTime", newCaseDetails.InstituteReachTime);
                cmd.Parameters.AddWithValue("@backToBaseTime", newCaseDetails.BackToBaseTime);
                cmd.Parameters.AddWithValue("@vehicleNo", newCaseDetails.VehicleDetails.VehicleNumber);
                cmd.Parameters.AddWithValue("@startOdo", newCaseDetails.StartOdo);
                cmd.Parameters.AddWithValue("@sceneReachOdo", newCaseDetails.SceneReachOdo);
                cmd.Parameters.AddWithValue("@instituteReachOdo", newCaseDetails.InstituteReachOdo);
                cmd.Parameters.AddWithValue("@baseReachOdo", newCaseDetails.BackToBaseOdo);
                cmd.Parameters.AddWithValue("@callTime", newCaseDetails.CallTime);

                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@victimName", "");
                cmd.Parameters.AddWithValue("@victimAge", 0);
                cmd.Parameters.AddWithValue("@victimGender", 0);
                cmd.Parameters.AddWithValue("@victimRemarks", "");
                cmd.Parameters.AddWithValue("@chiefComplaintId", 0);
                cmd.Parameters.AddWithValue("@victimId", 0);
                cmd.Parameters.AddWithValue("@standardRemarksId", newCaseDetails.StandardRemark);

                foreach (var victim in newCaseDetails.VictimsDetails)
                {
                    cmd.CommandText = "SaveNewCaseVictimDetails";
                    cmd.Parameters["@victimName"].Value = victim.Name;
                    cmd.Parameters["@victimAge"].Value = victim.Age;
                    cmd.Parameters["@victimGender"].Value = victim.Gender;
                    cmd.Parameters["@victimRemarks"].Value = victim.Remarks;
                    cmd.Parameters["@chiefComplaintId"].Value = victim.ChiefComplaint;
                    cmd.Parameters["@victimId"].Value = victim.VictimId;

                    cmd.ExecuteNonQuery();
                    success = true;
                }

                conn.Close();
            }
            catch (Exception e)
            {

            }
            return success;
        }
    }
}
