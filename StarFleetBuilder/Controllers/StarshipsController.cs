// StarshipsController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarFleetBuilder.Data;
using StarFleetBuilder.Models;
using StarFleetBuilder.Services;

namespace StarFleetBuilder.Controllers
{
    public class StarshipsController : Controller
    {
        private readonly StarFleetBuilderContext _context;
        private readonly StarshipService _starshipService;
        private readonly UserService _userService;

        public StarshipsController(StarFleetBuilderContext context, StarshipService starship, UserService userService)
        {
            _context = context;
            _starshipService = starship;
            _userService = userService;
        }

        // GET: Starships
        public async Task<IActionResult> Index()
        {
            var starships = await _context.Starship.ToListAsync();
            return View(starships);
        }

        // GET: Starships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Starship ID cannot be null.");
            }

            var starship = await _context.Starship.FirstOrDefaultAsync(m => m.Id == id);
            if (starship == null)
            {
                return NotFound("Starship not found.");
            }

            return View(starship);
        }

        // GET: Starships/Create
        public async Task<IActionResult> Create(string? url = null)
        {
            Starship starship;

            if (string.IsNullOrEmpty(url))
            {
                starship = await _starshipService.GetRandomStarshipAsync();
            }
            else
            {
                starship = await _starshipService.GetStarshipByUrlAsync(url); 
            }

            return View(starship);
        }

        // POST: Starships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Starship starship)
        {
            if (ModelState.IsValid)
            {
                if (int.TryParse(starship.CostInCredits, out int costInCredits))
                {
                    if (await _userService.SubtractCreditsAsync(costInCredits))
                    {
                        _context.Add(starship);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "You do not have enough credits to purchase this starship.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid cost format.");
                }
            }

            return View(starship); 
        }

        // GET: Starships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("Starship ID cannot be null.");
            }

            var starship = await _context.Starship.FindAsync(id);
            if (starship == null)
            {
                return NotFound("Starship not found.");
            }

            return View(starship);
        }

        // POST: Starships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Manufacturer,StarshipClass,Length,Crew,Passengers,HyperdriveRating,MGLT,CargoCapacity,Consumables,CostInCredits,MaxAtmospheringSpeed,Created,Edited,Url")] Starship starship)
        {
            if (id != starship.Id)
            {
                return NotFound("Starship ID mismatch.");
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
                        return NotFound("Starship no longer exists.");
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
                return NotFound("Starship ID cannot be null.");
            }

            var starship = await _context.Starship.FirstOrDefaultAsync(m => m.Id == id);
            if (starship == null)
            {
                return NotFound("Starship not found.");
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
                await _userService.AddCreditsAsync(int.Parse(starship.CostInCredits));
                _context.Starship.Remove(starship);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StarshipExists(int id)
        {
            return _context.Starship.Any(e => e.Id == id);
        }
    }
}
