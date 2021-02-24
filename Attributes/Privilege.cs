using System.Web.Mvc;
using System.Web;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Attributes {
    public class PrivilegeAttribute : TypeFilterAttribute
    {
        public PrivilegeAttribute(string claimValue) : base(typeof(PrivilegeFilter))
        {
            Arguments = new object[] {new Claim("Privilege", claimValue };
        }
    }

    public class PrivilegeFilter : Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        readonly Claim _claim;

        public PrivilegeFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}