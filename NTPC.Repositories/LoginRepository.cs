using NTPC.DAL;
using NTPC.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace NTPC.Repositories
{
    public class LoginRepository : ILoginActions
    {
        public Login VerifyUser(Login login)
        {
            Login loggedInUser = new Login();
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
                cmd.CommandText = "`ntpc_acl`.`VerifyLoginDetails`";
                cmd.Parameters.AddWithValue("@userId", login.UserName);
                cmd.Parameters.AddWithValue("@_password", login.Password);

                ad.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<string> userRoles = new List<string>();
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var role = ds.Tables[0].Rows[i]["role_name"].ToString();
                        userRoles.Add(role);
                    }
                    loggedInUser.UserName = ds.Tables[0].Rows[0]["user_name"].ToString();
                    loggedInUser.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"]);
                    loggedInUser.RoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["role_id"]);
                    loggedInUser.LicenseRenewalDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["licence_renewal_date"]);
                    loggedInUser.UserRoles = userRoles;
                }
            }
            catch (Exception e)
            {

            }
            return loggedInUser;
        }
    }
}
