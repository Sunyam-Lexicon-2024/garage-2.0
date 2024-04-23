using Garage_2_0.Models;
using Garage_2_0.Models.ViewModels.Garage;
using Garage_2_0.Repositories;
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

            var model = garages.Select(g => new GarageViewModel
            {
                Id = g.Id,
                Name = g.Name,
                MaxCapacity = g.MaxCapacity
            });

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = (await _repository.Find(g => g.Id == id)).Single();

            if (garage == null)
            {
                return NotFound();
            }

            GarageViewModel garageViewModel = new()
            {
                Id = garage.Id,
                Name = garage.Name,
                MaxCapacity = garage.MaxCapacity
            };

            return View(garageViewModel);
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

            var garage = (await _repository.Find(g => g.Id == id)).Single();

            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateOrEditGarageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var garage = (await _repository.Find(x => x.Id == viewModel.Id)).Single();
            if (garage is null)
            {
                return NotFound();
            }

            garage.Name = viewModel.Name;
            garage.MaxCapacity = viewModel.MaxCapacity;

            await _repository.Update(garage);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garageToDelete = await _repository.Delete(id);
            if (garageToDelete == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GarageExists(int id)
        {
            return await _repository.Any(g => g.Id == id);
        }
    }
}
