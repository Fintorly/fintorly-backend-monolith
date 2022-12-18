using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fintorly.Application.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddPhone(this ICollection<Claim> claims, string phone)
        {
            claims.Add(new Claim(ClaimTypes.MobilePhone, phone));
        }
        public static void AddIpAddress(this ICollection<Claim> claims, string ipAddress)
        {
            claims.Add(new Claim("IpAddress", ipAddress));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
        public static void AddIsDeletedStatus(this ICollection<Claim> claims, bool isDeleted)
        {
            claims.Add(new Claim("IsDeleted", isDeleted.ToString()));
        }
    }
}

