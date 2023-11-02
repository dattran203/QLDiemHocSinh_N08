using Microsoft.AspNetCore.Mvc;
using QLDiem.Models.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;


namespace QLDiem.Controllers
{
    [Route("homeadmin")]
    public class HomeAdminController : Controller
    {
        [Route("")]
        [Route("index")]

        [Authentication]
        

        public IActionResult Index()
        {
            return View();
        }
    }
}
