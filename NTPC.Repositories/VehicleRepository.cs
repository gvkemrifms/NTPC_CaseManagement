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
    public class VehicleRepository : IVehicleDetails
    {
        public List<Manufacturer> GetManufacturers()
        {
            MySqlConnection Con = new MySqlConnection(ConfigurationManager.ConnectionStrings["gvkemricon"].ConnectionString);
            MySqlCommand Cmd = null;
            MySqlDataReader dr = null;
            using (Con)
            {
                Con.Open();
                Cmd = new MySqlCommand("SELECT * FROM m_manufacture ORDER BY manufacturer_name ASC", Con);
                Cmd.CommandType = CommandType.Text;
                dr = Cmd.ExecuteReader();

                List<Manufacturer> manufacturers = new List<Manufacturer>();
                while (dr.Read())
                {
                    Manufacturer manufacturer = new Manufacturer();
                    manufacturer.ManufacturerId = Convert.ToInt16(dr["makeid"]);
                    manufacturer.ManufacturerName = dr["manufacturer_name"].ToString();

                    manufacturers.Add(manufacturer);
                }
                Con.Close();
                return manufacturers;
            }
        }

        public bool SaveSparePartDetails(int manufacturerId, string partName, int cost)
        {
            return true;
        }
    }
}
