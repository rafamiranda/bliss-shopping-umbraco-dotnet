using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetClaimTypeValue(this IHttpContextAccessor httpContextAccessor, string claimType)
        {
            return httpContextAccessor?.HttpContext?.User?.GetClaimTypeValue(claimType);
        }

        public static int? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.GetUserId();
        }
    }
}
