using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApps.Common
{
    public interface IUserClaimsManager
    {
        //List<string> GetUserClaims();
        bool CheckUserClaim(string claim);
    }
}
