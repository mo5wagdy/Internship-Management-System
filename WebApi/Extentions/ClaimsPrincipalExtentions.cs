using System;
using System.Security.Claims;

namespace WebApi.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        //Tries Standard Claim Types Used By Many JWT Implementations
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            if (user == null) return null;

            string? id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("id")?.Value
                ?? user.FindFirst("sub")?.Value;

            if (Guid.TryParse(id, out var guid))
                return guid;
            return null;

        }
    }
}
