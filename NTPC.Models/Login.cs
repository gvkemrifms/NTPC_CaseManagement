using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.Models
{
    public class Login
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime LicenseRenewalDate { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }

        public List<string> UserRoles { get; set; }
    }
}
