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
    [Authorize]

    public class KnjigaController : Controller
    {
        private readonly Context _context;

        public KnjigaController(Context context)
        {
            _context = context;
        }

        // GET: Knjiga
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NaslovSortParm"] = sortOrder == "Naslov" ? "Naslov_desc" : "Naslov";
            ViewData["StStraniSortParm"] = sortOrder == "StStrani" ? "StStrani_desc" : "StStrani";
            ViewData["StZnakovSortParm"] = sortOrder == "StZnakov" ? "StZnakov_desc" : "StZnakov";
            ViewData["CenaSortParm"] = sortOrder == "Cena" ? "Cena_desc" : "Cena";

            var knjigeQuery = from s in _context.Knjiga
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                knjigeQuery = knjigeQuery.Where(s => s.Naslov.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Naslov_desc":
                    knjigeQuery = knjigeQuery.OrderByDescending(s => s.Naslov);
                    break;
                case "StStrani":
                    knjigeQuery = knjigeQuery.OrderBy(s => s.StStrani);
                    break;
                case "StStrani_desc":
                    knjigeQuery = knjigeQuery.OrderByDescending(s => s.StStrani);
                    break;
                case "StZnakov":
                    knjigeQuery = knjigeQuery.OrderBy(s => s.StZnakov);
                    break;
                case "StZnakov_desc":
                    knjigeQuery = knjigeQuery.OrderByDescending(s => s.StZnakov);
                    break;
                case "Cena":
                    knjigeQuery = knjigeQuery.OrderBy(s => s.Cena);
                    break;
                case "Cena_desc":
                    knjigeQuery = knjigeQuery.OrderByDescending(s => s.Cena);
                    break;
                default:
                    knjigeQuery = knjigeQuery.OrderBy(s => s.Naslov);
                    break;
            }

            var knjige = await knjigeQuery.ToListAsync();
            var knjigaViewModels = new List<KnjigaViewModel>();

            foreach (var knjiga in knjige)
            {
                var avtor = await _context.Avtor.FirstOrDefaultAsync(a => a.AvtorID == knjiga.AvtorID);
                var zanr = await _context.Zanr.FirstOrDefaultAsync(a => a.ZanrID == knjiga.ZanrID);
                if (avtor != null && zanr != null)
                {
                    knjigaViewModels.Add(new KnjigaViewModel
                    {
                        KnjigaID = knjiga.KnjigaID,
                        Naslov = knjiga.Naslov,
                        StZnakov = knjiga.StZnakov,
                        StStrani = knjiga.StStrani,
                        Cena = knjiga.Cena,
                        ZanrID = knjiga.ZanrID,
                        AvtorIme = avtor.Ime + " " + avtor.Priimek,
                        Zanr = zanr.ImeZanra
                    });
                }
            }

            return View(knjigaViewModels);
        }



        // GET: Knjiga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjiga
                .FirstOrDefaultAsync(m => m.KnjigaID == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // GET: Knjiga/Create
        public IActionResult Create()
        {
            ViewData["AvtorID"] = new SelectList(_context.Avtor, "AvtorID", "Ime");
            ViewData["ZanrID"] = new SelectList(_context.Zanr, "ZanrID", "ImeZanra");
            return View();
        }

        // POST: Knjiga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KnjigaID,Naslov,Cena,ZanrID,AvtorID,Besedilo")] Knjiga knjiga){
            if (ModelState.IsValid)
            {
                // Izračunaj število znakov in strani iz besedila knjige
                knjiga.StZnakov = knjiga.Besedilo.Length;
                knjiga.StStrani = (int)Math.Ceiling((double)knjiga.StZnakov / 400); // Predpostavka: 400 znakov na stran
                knjiga.Cena = (float)((knjiga.StStrani * 0.01) + 1);

                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvtorID"] = new SelectList(_context.Avtor, "AvtorID", "Ime", knjiga.AvtorID);
            ViewData["ZanrID"] = new SelectList(_context.Zanr, "ZanrID", "ImeZanra", knjiga.ZanrID);
            return View(knjiga);
        }

        // GET: Knjiga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjiga.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }
            ViewData["AvtorID"] = new SelectList(_context.Avtor, "AvtorID", "Ime", knjiga.AvtorID);
            ViewData["ZanrID"] = new SelectList(_context.Zanr, "ZanrID", "ImeZanra", knjiga.ZanrID);
            return View(knjiga);
        }

        // POST: Knjiga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KnjigaID,Naslov,StStrani,StZnakov,Cena,ZanrID,AvtorID,Besedilo")] Knjiga knjiga)
        {
            if (id != knjiga.KnjigaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knjiga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnjigaExists(knjiga.KnjigaID))
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
            ViewData["AvtorID"] = new SelectList(_context.Avtor, "AvtorID", "Ime", knjiga.AvtorID);
            ViewData["ZanrID"] = new SelectList(_context.Zanr, "ZanrID", "ImeZanra", knjiga.ZanrID);
            return View(knjiga);
        }

        // GET: Knjiga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjiga
                .FirstOrDefaultAsync(m => m.KnjigaID == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // POST: Knjiga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knjiga = await _context.Knjiga.FindAsync(id);
            if (knjiga != null)
            {
                _context.Knjiga.Remove(knjiga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnjigaExists(int id)
        {
            return _context.Knjiga.Any(e => e.KnjigaID == id);
        }
    }
}
