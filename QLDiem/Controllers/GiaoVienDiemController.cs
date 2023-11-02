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
    public class GiaoVienDiemController : Controller
    {

        private readonly QLDiemContext _context;

        public GiaoVienDiemController(QLDiemContext context)
        {
            _context = context;
        }
        [Authentication]
        public IActionResult Index(int currentpage)
        {
            var Update = _context.HocSinhDiems.Where(e => e.DiemTbm == null).ToList();

            foreach (var item in Update)
            {
                var newDiemTbm = Math.Round((decimal)(item.Diem15p + item.DiemMieng + 2 * item.Diem1Tiet + 3 * item.DiemGk + 4 * item.DiemthiHk) / 11, 1);
                item.DiemTbm = (double)newDiemTbm;
            }
            _context.SaveChanges();
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var lop = _context.Lops.FirstOrDefault(l => l.Malop == malop).Tenlop;
            ViewBag.MaLop = lop;
            int sizes = 10; // số phần tử của 1 trang

            var hocSinhDiem = _context.HocSinhDiems
                .Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation).ToList();
            ViewBag.PageSize = sizes;// truyền page size sang  sang view truyền vào thẻ a để thực hiện sự kiện load.
            ViewBag.pageCount = (int)Math.Ceiling((double)hocSinhDiem.Count / sizes);// tổng số trang       
            hocSinhDiem = hocSinhDiem.Take(sizes).ToList();
            return View(hocSinhDiem);
        }

        [HttpPost]
        public IActionResult Search(string keyword, string monHocId, string kyHocId, string namHocId)
        {
            //var hocSinhKy = _context.HocSinhDiems
            //        .Where(h => h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
            //        .Include(h => h.MahocsinhNavigation)
            //        .Include(h => h.MamonNavigation)
            //        .Include(h => h.NamHocIDNavigation)
            //        .ToList();
            int kyHocIdInt = int.Parse(kyHocId);
            int namHocIdInt = int.Parse(namHocId);
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var hocSinhKy = _context.HocSinhDiems
                .Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation).ToList();
            if (namHocId == "0")
            {
                if (monHocId == "0" && kyHocIdInt != 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Kyhoc == kyHocIdInt && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId != "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Mamon == monHocId && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId != "0" && kyHocIdInt != 0)
                {
                    var hocSinhDiem = hocSinhKy
                   .Where(h => h.Mamon == monHocId && h.Kyhoc == kyHocIdInt && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                   .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else
                {
                    var hocSinhDiem = hocSinhKy
                   .Where(h => h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                   .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }

            }
            else
            {
                if (monHocId == "0" && kyHocIdInt != 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Kyhoc == kyHocIdInt && h.NamHocID == namHocIdInt && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId != "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Mamon == monHocId && h.NamHocID == namHocIdInt && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId == "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.NamHocID == namHocIdInt)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else
                {
                    var hocSinhDiem = hocSinhKy
                   .Where(h => h.Mamon == monHocId && h.Kyhoc == kyHocIdInt && h.NamHocID == namHocIdInt && h.MahocsinhNavigation.TenHs.ToLower().Contains(keyword.ToLower()))
                   .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
            }
        }

        public IActionResult GetPage(int page, int pageSize, string maMon)
        {
            // lý do không cập nhật được currentpage ở đây vì nó trả kêt quả ra parialview
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop; var totalRecords = _context.HocSinhDiems
                .Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation).Count();
            var pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > pageCount)
            {
                page = pageCount;
            }
            var hocSinhDiem = _context.HocSinhDiems
                .Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation)
                .Skip((page - 1) * pageSize)// bỏ qua những thằng từ page trước lấy đủ 5 thằng
                .Take(pageSize)
                .ToList();

            return PartialView("_HocSinhDiemList", hocSinhDiem);
        }

        public IActionResult DiemByDuLieu(string monHocId, string kyHocId, string namHocId)
        {
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            int kyHocIdInt = int.Parse(kyHocId);
            int namHocIdInt = int.Parse(namHocId);
            var hocSinhKy = _context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop)
                    .Include(h => h.MahocsinhNavigation)
                    .Include(h => h.MamonNavigation)
                    .Include(h => h.NamHocIDNavigation);
            if (namHocId == "0")
            {
                if (monHocId == "0" && kyHocIdInt != 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Kyhoc == kyHocIdInt)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId != "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Mamon == monHocId)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else
                {
                    var hocSinhDiem = hocSinhKy
                   .Where(h => h.Mamon == monHocId && h.Kyhoc == kyHocIdInt)
                   .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }

            }
            else
            {
                if (monHocId == "0" && kyHocIdInt != 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Kyhoc == kyHocIdInt && h.NamHocID == namHocIdInt)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId != "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.Mamon == monHocId && h.NamHocID == namHocIdInt)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else if (monHocId == "0" && kyHocIdInt == 0)
                {
                    var hocSinhDiem = hocSinhKy
                    .Where(h => h.NamHocID == namHocIdInt)
                    .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
                else
                {
                    var hocSinhDiem = hocSinhKy
                   .Where(h => h.Mamon == monHocId && h.Kyhoc == kyHocIdInt && h.NamHocID == namHocIdInt)
                   .ToList();
                    return PartialView("_HocSinhDiemList", hocSinhDiem);
                }
            }

            //     var hocSinhDiem = _context.HocSinhDiems
            //    .Where(h => h.Mamon == monHocId && h.Kyhoc == kyHocIdInt)
            //    .Include(h => h.MahocsinhNavigation)
            //    .Include(h => h.MamonNavigation)
            //    .ToList();

            //int sizes = 10; // Số phần tử của 1 trang
            //ViewBag.PageSize = sizes; // Truyền page size sang view để thực hiện sự kiện load.
            //ViewBag.pageCount = (int)Math.Ceiling((double)hocSinhDiem.Count / sizes); // Tổng số trang
            //hocSinhDiem = hocSinhDiem.ToList();

            //return PartialView("_HocSinhDiemList", hocSinhDiem);
        }
        // GET: HocSinhDiem/Details/5
        [Authentication]

        public async Task<IActionResult> Details(string MaHs, string Mamon, int Kyhoc)
        {
            if (Mamon == null || Mamon == null)
            {
                return NotFound();
            }
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var hocSinhDiem = await _context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation)
                .FirstOrDefaultAsync(m => m.Mahocsinh == MaHs && m.Mamon == Mamon && m.Kyhoc == Kyhoc);
            return View(hocSinhDiem);
        }

        // GET: HocSinhDiem/Create
        [Authentication]


        public IActionResult Create()
        {
            var hocSinhDiemMaHsList = _context.HocSinhDiems.Select(hd => hd.Mahocsinh).Distinct().ToList();
            //var hocSinhMaHsList = _context.HocSinhs.Select(hs => hs.MaHs).ToList();

            //var maHsNotInHocSinhDiem = hocSinhMaHsList.Except(hocSinhDiemMaHsList).ToList();

            // Truyền danh sách đã lọc vào ViewData
            ViewData["Mahocsinh"] = new SelectList(hocSinhDiemMaHsList);
            ViewData["Mamon"] = new SelectList(_context.MonHocs, "Mamon", "Tenmon");
            ViewData["NamHocID"] = new SelectList(_context.NamHocs, "NamHocId", "NamHoc1");
            return View();
        }

        // POST: HocSinhDiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahocsinh,Kyhoc,Mamon,NamHocID,Diem15p,DiemMieng,Diem1Tiet,DiemGk,DiemthiHk")] HocSinhDiem hocSinhDiem)
        {
            _context.Add(hocSinhDiem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(hocSinhDiem);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["Mahocsinh"] = new SelectList(_context.HocSinhs, "MaHs", "MaHs", hocSinhDiem.Mahocsinh);
            //ViewData["Mamon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", hocSinhDiem.Mamon);
            //ViewData["NamHocID"] = new SelectList(_context.NamHocs, "NamHocId", "NamHocId", hocSinhDiem.NamHocID);

            //return View(hocSinhDiem);
        }

        // GET: HocSinhDiem/Edit/5
        [Authentication]

        public async Task<IActionResult> Edit(string MaHs, string Mamon, int Kyhoc)
        {
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var hocSinhDiem = await _context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation)
                .FirstOrDefaultAsync(m => m.Mahocsinh == MaHs && m.Mamon == Mamon && m.Kyhoc == Kyhoc);
            if (hocSinhDiem == null)
            {
                return NotFound();
            }
            ViewData["Mahocsinh"] = new SelectList(_context.HocSinhs, "MaHs", "MaHs", hocSinhDiem.Mahocsinh);
            ViewData["Mamon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", hocSinhDiem.Mamon);
            return View(hocSinhDiem);
        }

        // POST: HocSinhDiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string MaHs, string Mamon, int Kyhoc, [Bind("Mahocsinh,Kyhoc,Mamon,NamHocID,Diem15p,DiemMieng,Diem1Tiet,DiemGk,DiemthiHk")] HocSinhDiem hocSinhDiem)
        {

            try
            {
                _context.Update(hocSinhDiem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocSinhDiemExists(hocSinhDiem.Mahocsinh, hocSinhDiem.Mamon, hocSinhDiem.Kyhoc))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //ViewData["Mahocsinh"] = new SelectList(_context.HocSinhs, "MaHs", "MaHs", hocSinhDiem.Mahocsinh);
            //ViewData["Mamon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", hocSinhDiem.Mamon);
            return View(hocSinhDiem);
        }

        // GET: HocSinhDiem/Delete/5
        [Authentication]

        public async Task<IActionResult> Delete(string MaHs, string Mamon, int Kyhoc)
        {

            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var hocSinhDiem = await _context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation)
                .FirstOrDefaultAsync(m => m.Mahocsinh == MaHs && m.Mamon == Mamon && m.Kyhoc == Kyhoc);
            if (hocSinhDiem == null)
            {
                return NotFound();
            }

            return View(hocSinhDiem);
        }

        // POST: HocSinhDiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string MaHs, string Mamon, int Kyhoc)
        {
            if (_context.HocSinhDiems == null)
            {
                return Problem("Entity set 'QLDiemContext.HocSinhDiems'  is null.");
            }
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            var hocSinhDiem = await _context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop)
                .Include(h => h.MahocsinhNavigation)
                .Include(h => h.MamonNavigation)
                .Include(h => h.NamHocIDNavigation)
                .FirstOrDefaultAsync(m => m.Mahocsinh == MaHs && m.Mamon == Mamon && m.Kyhoc == Kyhoc);
            if (hocSinhDiem != null)
            {
                _context.HocSinhDiems.Remove(hocSinhDiem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocSinhDiemExists(string MaHs, string Mamon, int Kyhoc)
        {
            var mgv = HttpContext.Session.GetString("MGV1");
            var malop = _context.Lops.FirstOrDefault(lop => lop.Gvcn == mgv as string)?.Malop;
            return (_context.HocSinhDiems.Where(h => h.MahocsinhNavigation.Malop == malop).Any(e => e.Mahocsinh == MaHs && e.Mamon == Mamon && e.Kyhoc == Kyhoc));
        }
    }
}
