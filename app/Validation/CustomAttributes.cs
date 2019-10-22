using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Attributes
{
    public class AuthorizeOrRedirect : ActionFilterAttribute
    {
        private readonly string _route;
        private readonly string _role;

        public AuthorizeOrRedirect(string route, string role)
        {
            _route = route;
            _role = role;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsInRole(_role))
            {
                filterContext.Result = new RedirectResult(_route);
            }
            else
            {
                base.OnResultExecuting(filterContext);
            }
        }
    }
}