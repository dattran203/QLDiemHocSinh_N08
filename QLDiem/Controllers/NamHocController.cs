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
    public class NamHocController : Controller
    {
        private readonly QLDiemContext _context;

        public NamHocController(QLDiemContext context)
        {
            _context = context;
        }
        [Authentication]
        // GET: NamHoc
        public async Task<IActionResult> Index()
        {
              return _context.NamHocs != null ? 
                          View(await _context.NamHocs.ToListAsync()) :
                          Problem("Entity set 'QLDiemContext.NamHocs'  is null.");
        }

        // GET: NamHoc/Create
        public IActionResult Create()
        {
            var lastNamHoc = _context.NamHocs.OrderByDescending(nh => nh.NamHocId).FirstOrDefault();
            int nextNamHocId = 1;
            if (lastNamHoc != null)
            {
                nextNamHocId = lastNamHoc.NamHocId + 1;
            }

            ViewBag.NamHocID = nextNamHocId;
            return View();
        }

        // POST: NamHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NamHocId,NamHoc1")] NamHoc namHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(namHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(namHoc);
        }

        // GET: NamHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NamHocs == null)
            {
                return NotFound();
            }

            var namHoc = await _context.NamHocs.FindAsync(id);
            if (namHoc == null)
            {
                return NotFound();
            }
            return View(namHoc);
        }

        // POST: NamHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NamHocId,NamHoc1")] NamHoc namHoc)
        {
            if (id != namHoc.NamHocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(namHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NamHocExists(namHoc.NamHocId))
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
            return View(namHoc);
        }

        private bool NamHocExists(int id)
        {
          return (_context.NamHocs?.Any(e => e.NamHocId == id)).GetValueOrDefault();
        }
    }
}
