using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarFleetBuilder.Data;
using StarFleetBuilder.Models;

namespace StarFleetBuilder.Controllers
{
    public class StarshipsController : Controller
    {
        private readonly StarFleetBuilderContext _context;

        public StarshipsController(StarFleetBuilderContext context)
        {
            _context = context;
        }

        // GET: Starships
        public async Task<IActionResult> Index()
        {
            return View(await _context.Starship.ToListAsync());
        }

        // GET: Starships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var starship = await _context.Starship
                .FirstOrDefaultAsync(m => m.Id == id);
            if (starship == null)
            {
                return NotFound();
            }

            return View(starship);
        }

        // GET: Starships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Starships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Manufacturer,StarshipClass,Length,Crew,Passengers,HyperdriveRating,MGLT,CargoCapacity,Consumables,CostInCredits,MaxAtmospheringSpeed,Created,Edited,Url")] Starship starship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(starship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(starship);
        }

        // GET: Starships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var starship = await _context.Starship.FindAsync(id);
            if (starship == null)
            {
                return NotFound();
            }
            return View(starship);
        }

        // POST: Starships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Manufacturer,StarshipClass,Length,Crew,Passengers,HyperdriveRating,MGLT,CargoCapacity,Consumables,CostInCredits,MaxAtmospheringSpeed,Created,Edited,Url")] Starship starship)
        {
            if (id != starship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarshipExists(starship.Id))
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
            return View(starship);
        }

        // GET: Starships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var starship = await _context.Starship
                .FirstOrDefaultAsync(m => m.Id == id);
            if (starship == null)
            {
                return NotFound();
            }

            return View(starship);
        }

        // POST: Starships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var starship = await _context.Starship.FindAsync(id);
            if (starship != null)
            {
                _context.Starship.Remove(starship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarshipExists(int id)
        {
            return _context.Starship.Any(e => e.Id == id);
        }
    }
}
