using garage_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garage_2._0.Controllers
{
    public class GarageController(GarageDbContext context) : Controller
    {

        private readonly GarageDbContext _context = context;

        // use GarageViewModel?
        public IActionResult Index()
        {
            return View();
        }

        // use GarageDetailedViewModel
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages.FirstOrDefaultAsync(g => g.ID == id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // use GarageDetailedViewModel
        public IActionResult Create()
        {
            return View();
        }

        // use GarageDetailedViewModel
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages.FirstOrDefaultAsync(g => g.ID == id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // use GarageDetailedViewModel
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,MaxCapacity")] Garage garage)
        {
            if (id != garage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageExists(garage.ID))
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
            return View(garage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garage = await _context.Garages.FindAsync(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> List()
        {
            var garages = await _context.Garages.ToListAsync();

            // use GarageViewModel

            return View(garages);
        }
        private bool GarageExists(int id)
        {
            return _context.Garages.Any(g => g.ID == id);
        }
    }
}
