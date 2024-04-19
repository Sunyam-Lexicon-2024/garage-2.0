using Garage_2_0.Models;
using Garage_2_0.Repositories;
using Garage_2_0.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Controllers
{

    public class GarageController(IRepository<Garage> repository) : Controller
    {
        private readonly IRepository<Garage> _repository = repository;

        public async Task<IActionResult> Index()
        {
            var garages = await _repository.All();

            var model = garages.Select(x => new GarageViewModel
            {
                Id = x.ID,
                Name = x.Name,
                MaxCapacity = x.MaxCapacity
            });

            return View(model);
        }

        // use GarageDetailedViewModel
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _repository.Find(g => g.ID == id);

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

            var garage = await _repository.Find(g => g.ID == id);

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
                    if (!await _repository.Any(g => g.ID == id))
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
    }
}
