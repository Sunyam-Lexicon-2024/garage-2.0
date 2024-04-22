﻿using Garage_2_0.Models;
using Garage_2_0.Models.ViewModels;
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
                ParkingSpotCount = g.ParkingSpotCount
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
                ParkingSpotCount = garage.ParkingSpotCount
            };

            return View(garageViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GarageViewModel viewModel)
        {
            var garageToCreate = new Garage()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                ParkingSpotCount = viewModel.ParkingSpotCount
            };

            for (int i = 0; i < garageToCreate.ParkingSpotCount; i++)
            {
                garageToCreate.ParkingSpots.Add(new ParkingSpot());
            };

            var createdGarage = await _repository.Create(garageToCreate);

            if (createdGarage is null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,MaxCapacity")] Garage garage)
        {
            if (id != garage.Id)
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
                    if (!await GarageExists(id))
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
