using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Fintorly.Infrastructure.Utilities.Security
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}

