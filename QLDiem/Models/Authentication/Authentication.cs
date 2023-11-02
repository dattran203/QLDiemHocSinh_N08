using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;

namespace QLDiem.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("TaiKhoan")==null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","Access" },
                    {"Action","Login" }
                });
            }
        }
    }
}
