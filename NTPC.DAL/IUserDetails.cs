using NTPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.DAL
{
    public interface IUserDetails
    {
        List<Vehicle> GetUserStateRelatedVehicles(int userId);

        void GetUserPendingCases(int userId, string agentId);
    }
}
