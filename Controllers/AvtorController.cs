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
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AvtorController : Controller
    {
        private readonly Context _context;

        public AvtorController(Context context)
        {
            _context = context;
        }

        // GET: Avtor
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Avtor.ToListAsync());
        // }
        public async Task<IActionResult> Index(string sortOrder)
{
    ViewData["ImeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
    ViewData["PriimekSortParm"] = sortOrder == "Priimek" ? "Priimek_desc" : "Priimek";
    var students = from s in _context.Avtor
                   select s;
    switch (sortOrder)
    {
        case "Ime_desc":
            students = students.OrderByDescending(s => s.Ime);
            break;
        case "Priimek":
            students = students.OrderBy(s => s.Priimek);
            break;
        case "Priimek_desc":
            students = students.OrderByDescending(s => s.Priimek);
            break;
        default:
            students = students.OrderBy(s => s.Ime);
            break;
    }
    return View(await students.AsNoTracking().ToListAsync());
}

        // GET: Avtor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .FirstOrDefaultAsync(m => m.AvtorID == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // GET: Avtor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avtor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvtorID,Ime,Priimek")] Avtor avtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avtor);
        }

        // GET: Avtor/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor.FindAsync(id);
            if (avtor == null)
            {
                return NotFound();
            }
            return View(avtor);
        }

        // POST: Avtor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("AvtorID,Ime,Priimek")] Avtor avtor)
        {
            if (id != avtor.AvtorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avtor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvtorExists(avtor.AvtorID))
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
            return View(avtor);
        }

        // GET: Avtor/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .FirstOrDefaultAsync(m => m.AvtorID == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // POST: Avtor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avtor = await _context.Avtor.FindAsync(id);
            if (avtor != null)
            {
                _context.Avtor.Remove(avtor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvtorExists(int id)
        {
            return _context.Avtor.Any(e => e.AvtorID == id);
        }
    }
}
