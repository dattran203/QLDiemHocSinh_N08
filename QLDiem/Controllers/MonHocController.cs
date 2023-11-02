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
    public class MonHocController : Controller
    {
        private readonly QLDiemContext _context;

        public MonHocController(QLDiemContext context)
        {
            _context = context;
        }
        [Authentication]
        // GET: MonHoc
        public async Task<IActionResult> Index()
        {
              return _context.MonHocs != null ? 
                          View(await _context.MonHocs.ToListAsync()) :
                          Problem("Entity set 'QLDiemContext.MonHocs'  is null.");
        }

        public IActionResult Create()
        {
            var lastMonHoc = _context.MonHocs.OrderByDescending(mh => mh.Mamon).FirstOrDefault();
            int nextMonHocId = 1;
            if (lastMonHoc != null)
            {
                int lastId = int.Parse(lastMonHoc.Mamon.Substring(2));
                nextMonHocId = lastId + 1;
            }
            string newMamon = "MH" + nextMonHocId.ToString();
            while (MonHocExists(newMamon))
            {
                nextMonHocId++;
                newMamon = "MH" + nextMonHocId.ToString();
            }
            ViewBag.Mamon = newMamon;
            return View();
        }

        // POST: MonHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mamon,Tenmon")] MonHoc monHoc)
        {

            if (ModelState.IsValid)
            {
                _context.Add(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monHoc);
        }

        // GET: MonHoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MonHocs == null)
            {
                return NotFound();
            }

            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc == null)
            {
                return NotFound();
            }
            return View(monHoc);
        }

        // POST: MonHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mamon,Tenmon")] MonHoc monHoc)
        {
            if (id != monHoc.Mamon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonHocExists(monHoc.Mamon))
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
            return View(monHoc);
        }

        private bool MonHocExists(string id)
        {
          return (_context.MonHocs?.Any(e => e.Mamon == id)).GetValueOrDefault();
        }
    }
}
