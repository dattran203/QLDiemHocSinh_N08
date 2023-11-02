using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using QLDiem.Models;
using System.Text.RegularExpressions;

namespace QLDiem.Controllers
{
    public class AccessController : Controller
    {
        private readonly QLDiemContext _context;

        public AccessController(QLDiemContext context)
        {
            _context = context;
        }
        /*        QLDiemContext db=new QLDiemContext();
        */
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                return View();
            }
            else if (HttpContext.Session.GetString("TaiKhoan") == "admin")
            {
                return RedirectToAction("index", "homeadmin");
            }
            else
            {
                return RedirectToAction("index", "homegiaovien");
            }
        }

        [HttpPost]
        public IActionResult Login(TblUser user)
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                var u = _context.TblUsers.Where(x => x.TaiKhoan.Equals(user.TaiKhoan)
                && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    if (Regex.IsMatch(u.MaGv, @"^MGV\d+$"))
                    {
                        HttpContext.Session.SetString("MGV1", u.MaGv);
                        TempData["MGV"] = u.MaGv;
                        HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                        return RedirectToAction("index", "homegiaovien");
                        ViewBag.Message = "Bạn đã đang nhập thành công";
                    }
                    else
                    {
                        HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                        return RedirectToAction("index", "homeadmin");
                    }

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            return RedirectToAction("Login", "Access");
        }
    }
}
