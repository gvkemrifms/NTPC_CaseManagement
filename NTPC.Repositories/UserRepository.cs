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
    public class UserRepository : IUserDetails
    {
        public void GetUserPendingCases(int userId, string agentId)
        {
            throw new NotImplementedException();
        }

        public List<Vehicle> GetUserStateRelatedVehicles(int userId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
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
                cmd.CommandText = "GetUserStateVehicles";
                cmd.Parameters.AddWithValue("@userId", userId);

                ad.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.VehicleId = Convert.ToInt16(ds.Tables[0].Rows[i]["vehicle_id"]);
                        vehicle.VehicleNumber = ds.Tables[0].Rows[i]["vehicle_no"].ToString();
                        vehicle.State = ds.Tables[0].Rows[i]["state"].ToString();
                        vehicles.Add(vehicle);
                    }
                }
            }

            catch (Exception e)
            {

            }
            return vehicles;
        }
    }
}
