using System;
using System.Security.Claims;

namespace SimpleApp.WebApi
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var user = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            return new Guid(user.Value);
        }
    }
}
