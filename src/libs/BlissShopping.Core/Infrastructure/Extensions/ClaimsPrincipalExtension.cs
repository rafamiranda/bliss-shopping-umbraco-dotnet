using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetClaimTypeValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var aValue = string.Empty;

            switch (claimType)
            {
                case ClaimTypes.NameIdentifier:
                    aValue = GetUserId(claimsPrincipal).ToString();
                    break;
                case ClaimTypes.MobilePhone:
                    if (claimsPrincipal is null) throw new Exception(ClaimTypes.MobilePhone);
                    aValue = claimsPrincipal.FindFirst(ClaimTypes.MobilePhone)?.Value;
                    if (string.IsNullOrEmpty(aValue)) throw new Exception(ClaimTypes.MobilePhone);
                    break;
                default:
                    break;
            }

            return aValue;
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal is null) return -1;

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(userId, out var result) ? result : -1;
        }
    }
}

