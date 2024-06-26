using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioMeta2.Data;
using BiblioMeta2.Models;
using Microsoft.AspNetCore.Authorization;


namespace BiblioMeta2.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ZanrController : Controller
    {
        private readonly Context _context;

        public ZanrController(Context context)
        {
            _context = context;
        }

        // GET: Zanr
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["ZanrSortParam"] = sortOrder == "Zanr" ? "Zanr_desc" : "Zanr";
            var zanrQuery = from s in _context.Zanr
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                zanrQuery = zanrQuery.Where(s => s.ImeZanra.Contains(searchString));
            }

            switch(sortOrder){
                case "Zanr":
                    zanrQuery = zanrQuery.OrderBy(s => s.ImeZanra);
                    break;
                case "Zanr_desc":
                    zanrQuery = zanrQuery.OrderByDescending(s => s.ImeZanra);
                    break;
            }
            return View(await zanrQuery.AsNoTracking().ToListAsync());
        }

        // GET: Zanr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .FirstOrDefaultAsync(m => m.ZanrID == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // GET: Zanr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zanr/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZanrID,ImeZanra")] Zanr zanr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zanr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zanr);
        }

        // GET: Zanr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }
            return View(zanr);
        }

        // POST: Zanr/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZanrID,ImeZanra")] Zanr zanr)
        {
            if (id != zanr.ZanrID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zanr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZanrExists(zanr.ZanrID))
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
            return View(zanr);
        }

        // GET: Zanr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .FirstOrDefaultAsync(m => m.ZanrID == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // POST: Zanr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr != null)
            {
                _context.Zanr.Remove(zanr);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrID == id);
        }
    }
}
