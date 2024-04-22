using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Garage_2_0.Controllers
{
    public class VehicleController(IRepository<ParkedVehicle> repository) : Controller
    {
        private readonly IRepository<ParkedVehicle> _repository = repository;

        public async Task<IActionResult> Index(VehicleType? selectedVehicleType, string? regNumber)
        {
            var vehicles = await _repository.All();

            var viewModel = vehicles.Select(x => new ParkedVehicleSlimViewModel
            {
                Id = x.Id,
                RegistrationNumber = x.RegistrationNumber,
                Type = x.Type,
                Brand = x.Brand,
                RegisteredAt = x.RegisteredAt,
                Color = x.Color,
            }).ToList();

            if (selectedVehicleType is not null)
            {
                viewModel = viewModel.Where(m => m.Type == selectedVehicleType).ToList();
            }

            if (!string.IsNullOrEmpty(regNumber))
            {
                viewModel = [viewModel.FirstOrDefault(v => v.RegistrationNumber == regNumber)];
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _repository.Find(v => v.Id == id);
            var model = result.Select(x => new ParkedVehicleViewModel
            {
                Id = x.Id,
                Brand = x.Brand,
                Color = x.Color,
                Model = x.Model,
                RegisteredAt = x.RegisteredAt,
                RegistrationNumber = x.RegistrationNumber,
                Type = x.Type,
                Wheels = x.Wheels
            }).First();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParkedVehicle viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = new ParkedVehicle
                {
                    RegistrationNumber = viewModel.RegistrationNumber,
                    Type = viewModel.Type,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    Wheels = viewModel.Wheels,
                    RegisteredAt = viewModel.RegisteredAt,
                    Color = viewModel.Color
                };

                await _repository.Create(vehicle);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var vehicle = (await _repository.Find(v => v.Id == id)).Single();
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ParkedVehicle viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(viewModel);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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