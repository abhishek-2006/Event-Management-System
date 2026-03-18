using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementSystem.Filters
{
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                    .Any(em => em is AllowAnonymousAttribute);

            if (hasAllowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("Admin")))
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }

            // 2. PREVENT BROWSER CACHING
            context.HttpContext.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            context.HttpContext.Response.Headers.Pragma = "no-cache";
            context.HttpContext.Response.Headers.Expires = "0";

            base.OnActionExecuting(context);
        }
    }
}