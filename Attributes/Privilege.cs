using System.Web.Mvc;
using System.Web;
using System.Security.Claims;
using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Attributes {
    public class PrivilegeAttribute : TypeFilterAttribute
    {
        public PrivilegeAttribute(params string[] claimValues) : base(typeof(PrivilegeFilter))
        {
            Arguments = new object[] {new Claim("Privilege", string.Join(',', claimValues))};
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
            var privileges = _claim.Value.Split(',');
            var authorized = false;
            foreach(var privilege in privileges){
                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == privilege);
                if(hasClaim){
                    authorized = true;
                    break;
                }
            }
            if(!authorized){
                context.Result = new UnauthorizedResult();
            }
        }
    }
}