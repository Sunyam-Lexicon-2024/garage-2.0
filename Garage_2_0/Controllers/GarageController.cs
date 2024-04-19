using Garage_2_0.Models;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage_2_0.Controllers
{

    public class GarageController(IRepository<Garage> repository) : Controller
    {

        private readonly IRepository<Garage> _repository = repository;

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

            var garage = await _repository.Find(g => g.ID == id);

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Garage garage)
        {
            var createdGarage = await _repository.Create(garage);
            return View(createdGarage);
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
                    if (!await GarageExists(garage.ID))
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
        private async Task<bool> GarageExists(int id)
        {
            return await _repository.Any(g => g.ID == id);
        }
    }
}
