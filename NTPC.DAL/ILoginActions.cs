using NTPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTPC.DAL
{
   public interface ILoginActions
   {
       Login VerifyUser(Login login);
   }
}
