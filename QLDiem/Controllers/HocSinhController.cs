using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDiem.Models;
using QLDiem.Models.Authentication;

namespace QLDiem.Controllers
{
    public class HocSinhController : Controller
    {
        private readonly QLDiemContext _context;

        public HocSinhController(QLDiemContext context)
        {
            _context = context;
        }
        [Authentication]
        // GET: HocSinh
        public async Task<IActionResult> Index()
        {
            int sizes = 6; // số phần tử của 1 trang
            var qLDiemContext = _context.HocSinhs.Include(h => h.MalopNavigation).ToList();
            ViewBag.PageSize = sizes;// truyền page size sang  sang view truyền vào thẻ a để thực hiện sự kiện load.
            ViewBag.pageCount = (int)Math.Ceiling((double)qLDiemContext.Count / sizes);// tổng số trang       
            //ViewBag.MaMon = "";
            qLDiemContext = qLDiemContext.Take(sizes).ToList();
            return View(qLDiemContext);
        }
        public IActionResult GetPage(int page, int pageSize)
        {
            // lý do không cập nhật được currentpage ở đây vì nó trả kêt quả ra parialview
            var totalRecords = _context.HocSinhs
                .Include(h => h.MalopNavigation).Count();
            var pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > pageCount)
            {
                page = pageCount;
            }
            var hocSinh = _context.HocSinhs
                .Include(h => h.MalopNavigation)
                .Skip((page - 1) * pageSize)// bỏ qua những thằng từ page trước lấy đủ 5 thằng
                .Take(pageSize)
                .ToList();
            return PartialView("_HocSinhList", hocSinh);
        }

        public IActionResult GetPage2(int page, int pageSize)
        {
            // lý do không cập nhật được currentpage ở đây vì nó trả kêt quả ra parialview
            var mgv = TempData["MGV"] as string;
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == TempData["MGV"] as string)?.Malop;
            var totalRecords = _context.HocSinhs
                .Where(h => h.Malop == malop)
                .Include(h => h.MalopNavigation).Count();
            var pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > pageCount)
            {
                page = pageCount;
            }
            var hocSinh = _context.HocSinhs
                .Where(h => h.Malop == malop)
                .Include(h => h.MalopNavigation)
                .Skip((page - 1) * pageSize)// bỏ qua những thằng từ page trước lấy đủ 5 thằng
                .Take(pageSize)
                .ToList();
            return PartialView("_HocSinhList", hocSinh);
        }


        // GET: HocSinh/Details/5
        [Authentication]
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

        // GET: HocSinh/Create
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
            if (ModelState.IsValid)
            {
                _context.Add(hocSinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Malop"] = new SelectList(_context.Lops, "Malop", "Malop", hocSinh.Malop);
            return View(hocSinh);
        }

        // GET: HocSinh/Edit/5
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
        public async Task<IActionResult> Edit(string id, [Bind("MaHs,TenHs,Malop,Ngaysinh,Gioitinh,HotenBo,HotenMe,Diachi,Dienthoai,NgayvaoDoan,Ghichu")] HocSinh hocSinh)
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

        // GET: HocSinh/Delete/5
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HocSinhs == null)
            {
                return Problem("Entity set 'QLDiemContext.HocSinhs' is null.");
            }

            // Truy vấn và lấy ra các HocSinhDiem có Mahocsinh giống với MaHs
            var hocSinhDiemsToDelete = await _context.HocSinhDiems
                .Where(d => d.Mahocsinh == id)
                .ToListAsync();
            var KqHKsToDelete = await _context.KqHocKies
                .Where(d => d.MaHs == id)
                .ToListAsync();
            var KqNHsToDelete = await _context.KqNamHocs
                .Where(d => d.MaHs == id)
                .ToListAsync();
            // Loại bỏ các HocSinhDiem
            foreach (var hocSinhDiem in hocSinhDiemsToDelete)
            {
                _context.HocSinhDiems.Remove(hocSinhDiem);
            }
            foreach (var hocSinhDiem in KqHKsToDelete)
            {
                _context.KqHocKies.Remove(hocSinhDiem);

            }
            foreach (var hocSinhDiem in KqNHsToDelete)
            {
                _context.KqNamHocs.Remove(hocSinhDiem);
            }

            // Truy vấn và lấy ra HocSinh để xóa
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
