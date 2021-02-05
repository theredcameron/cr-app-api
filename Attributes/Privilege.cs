using System.Web.Mvc;
using System.Web;

namespace api.Attributes {
    public class PrivilegeAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if(filterContext.HttpContext.Session)
        }
    }
}