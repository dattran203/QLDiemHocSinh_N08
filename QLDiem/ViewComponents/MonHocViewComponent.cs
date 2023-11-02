using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLDiem.Models;
namespace QLDiem.ViewComponents
{
    public class MonHocViewComponent:ViewComponent
    {
        QLDiemContext db;
        List<MonHoc> monHocs;
        public MonHocViewComponent(QLDiemContext _context)
        {
            db = _context;
            monHocs = db.MonHocs.ToList();

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMonHoc", monHocs);
        }
    }
}
