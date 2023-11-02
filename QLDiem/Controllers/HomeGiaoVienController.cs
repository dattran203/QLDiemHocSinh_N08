using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDiem.Models;
using QLDiem.Models.Authentication;

namespace QLDiem.Controllers
{
    [Route("homegiaovien")]
    public class HomeGiaoVienController : Controller
    {
        private readonly QLDiemContext _context;
        public HomeGiaoVienController(QLDiemContext context)
        {
            _context = context;
        }


        [Authentication]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var mgv = HttpContext.Session.GetString("MGV1"); 
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv)?.Malop;
            var lop = _context.Lops.FirstOrDefault(l => l.Malop == malop).Tenlop;
            ViewBag.MaLop = lop;
            var hocsinh = _context.HocSinhs
                .Where(h => h.Malop == malop)
                .Include(h => h.MalopNavigation).ToList();
            return View(hocsinh);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HocSinhs == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs
                .Include(h => h.MalopNavigation)
                .FirstOrDefaultAsync(m => m.MaHs == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            //Tự tao mã
            // Lấy ra mã học sinh cuối cùng
            var lastHocSinh = _context.HocSinhs.OrderByDescending(hs => hs.MaHs).FirstOrDefault();
            int nextId = 1;
            if (lastHocSinh != null)
            {
                // Nếu có học sinh trong cơ sở dữ liệu, tăng mã tiếp theo lên 1
                int lastId = int.Parse(lastHocSinh.MaHs.Substring(2)); // Lấy phần số cuối cùng
                nextId = lastId + 1;
            }
            // Tạo mã học sinh cho học sinh mới
            string newMaHs = "HS" + nextId.ToString();
            while (HocSinhExists(newMaHs) == true)
            {
                nextId++;
                newMaHs = "HS" + nextId.ToString();
            }
            ViewBag.MaHS = newMaHs;
            ViewData["Malop"] = new SelectList(_context.Lops, "Malop", "Malop");
            return View();
        }

        // POST: HocSinh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHs,TenHs,Malop,Ngaysinh,Gioitinh,HotenBo,HotenMe,Diachi,Dienthoai,NgayvaoDoan,Ghichu")] HocSinh hocSinh)
        {
            _context.Add(hocSinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HocSinhs == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh == null)
            {
                return NotFound();
            }
            ViewData["Malop"] = new SelectList(_context.Lops, "Malop", "Malop", hocSinh.Malop);
            return View(hocSinh);
        }

        // POST: HocSinh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHs,TenHs,Ngaysinh,Gioitinh,HotenBo,HotenMe,Diachi,Dienthoai,NgayvaoDoan,Ghichu")] HocSinh hocSinh)
        {
            if (id != hocSinh.MaHs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocSinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocSinhExists(hocSinh.MaHs))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Malop"] = new SelectList(_context.Lops, "Malop", "Malop", hocSinh.Malop);
            return View(hocSinh);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HocSinhs == null)
            {
                return NotFound();
            }

            var hocSinh = await _context.HocSinhs
                .Include(h => h.MalopNavigation)
                .FirstOrDefaultAsync(m => m.MaHs == id);
            if (hocSinh == null)
            {
                return NotFound();
            }

            return View(hocSinh);
        }

        // POST: HocSinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HocSinhs == null)
            {
                return Problem("Entity set 'QLDiemContext.HocSinhs'  is null.");
            }
            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh != null)
            {
                _context.HocSinhs.Remove(hocSinh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocSinhExists(string id)
        {
            return (_context.HocSinhs?.Any(e => e.MaHs == id)).GetValueOrDefault();
        }
    }

}