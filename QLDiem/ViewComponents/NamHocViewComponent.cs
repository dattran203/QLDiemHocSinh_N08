using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLDiem.Models;
namespace QLDiem.ViewComponents
{
    public class NamHocViewComponent:ViewComponent
    {
        QLDiemContext db;
        List<NamHoc> namHocs;
        public NamHocViewComponent(QLDiemContext _context)
        {
            db = _context;
            namHocs=db.NamHocs.ToList();

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderNamHoc", namHocs);
        }
    }
}
