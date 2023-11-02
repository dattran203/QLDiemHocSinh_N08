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
    public class GiaoViensController : Controller
    {
        private readonly QLDiemContext _context;

        public GiaoViensController(QLDiemContext context)
        {
            _context = context;
        }
        [Authentication]
        // GET: GiaoViens
        public async Task<IActionResult> Index()
        {
            var qLDiemContext = _context.GiaoViens.Include(g => g.MaMonNavigation);
            return View(await qLDiemContext.ToListAsync());
        }

        // GET: GiaoViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GiaoViens == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens
                .Include(g => g.MaMonNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);
            if (giaoVien == null)
            {
                return NotFound();
            }

            return View(giaoVien);
        }

        // GET: GiaoViens/Create
        public IActionResult Create()
        {
            var lastGiaoVien = _context.GiaoViens.OrderByDescending(gv => gv.MaGv).FirstOrDefault();
            int nextGvId = 1;
            if (lastGiaoVien != null)
            {
                int lastId = int.Parse(lastGiaoVien.MaGv.Substring(3)); // Lấy phần số cuối cùng
                nextGvId = lastId + 1;
            }
            string newMaGv = "MGV" + nextGvId.ToString();
            while (GiaoVienExists(newMaGv))
            {
                nextGvId++;
                newMaGv = "MGV" + nextGvId.ToString();
            }
            ViewBag.MaGV = newMaGv;
            ViewData["MaMon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon");
            return View();
        }

        // POST: GiaoViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGv,TenGv,Ngaysinh,Gioitinh,MaMon,Diachi,Dienthoai")] GiaoVien giaoVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giaoVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaMon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", giaoVien.MaMon);
            return View(giaoVien);
        }

        // GET: GiaoViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GiaoViens == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens.FindAsync(id);
            if (giaoVien == null)
            {
                return NotFound();
            }
            ViewData["MaMon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", giaoVien.MaMon);
            return View(giaoVien);
        }

        // POST: GiaoViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGv,TenGv,Ngaysinh,Gioitinh,MaMon,Diachi,Dienthoai")] GiaoVien giaoVien)
        {
            if (id != giaoVien.MaGv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giaoVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiaoVienExists(giaoVien.MaGv))
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
            ViewData["MaMon"] = new SelectList(_context.MonHocs, "Mamon", "Mamon", giaoVien.MaMon);
            return View(giaoVien);
        }

        // GET: GiaoViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GiaoViens == null)
            {
                return NotFound();
            }

            var giaoVien = await _context.GiaoViens
                .Include(g => g.MaMonNavigation)
                .FirstOrDefaultAsync(m => m.MaGv == id);
            if (giaoVien == null)
            {
                return NotFound();
            }

            return View(giaoVien);
        }

        // POST: GiaoViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GiaoViens == null)
            {
                return Problem("Entity set 'QLDiemContext.GiaoViens'  is null.");
            }
            var GiaovienToDelete = await _context.TblUsers
                .Where(d => d.MaGv == id)
                .ToListAsync();
            foreach (var user in GiaovienToDelete)
            {
                _context.TblUsers.Remove(user);
            }
            var giaoVien = await _context.GiaoViens.FindAsync(id);
            if (giaoVien != null)
            {
                _context.GiaoViens.Remove(giaoVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiaoVienExists(string id)
        {
          return (_context.GiaoViens?.Any(e => e.MaGv == id)).GetValueOrDefault();
        }
    }
}
