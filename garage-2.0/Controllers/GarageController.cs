using garage_2._0.Models;
using garage_2._0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garage_2._0.Controllers
{


    public class GarageController(GarageDbContext context) : Controller
    {

        private readonly GarageRepository _repository = new(context);

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

            var garage = await _repository.GetById((int)id);

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

            var garage = await _repository.GetById((int)id);

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
                    await _repository.Update(garage);
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
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> List()
        {
            // use GarageViewModel
            var garages = await _repository.All();

            return View(garages);
        }
        private bool GarageExists(int id)
        {
            return _repository.Any(id);
        }
    }
}
