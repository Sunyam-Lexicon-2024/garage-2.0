using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Garage_2_0.Controllers
{
    public class VehicleController(
        IRepository<Garage> garageRepository,
        IRepository<ParkedVehicle> vehicleRepository) : Controller
    {
        private readonly IRepository<Garage> _garageRepository = garageRepository;
        private readonly IRepository<ParkedVehicle> _vehicleRepository = vehicleRepository;

        public async Task<IActionResult> Index(AlertViewModel? alert, VehicleType? selectedVehicleType, string? regNumber)
        {
            var viewModel = new IndexParkedVehicleViewModel();

            var vehicles = await _vehicleRepository.All();

            viewModel.ParkedVehicles = vehicles
                .OrderByDescending(v => v.RegisteredAt)
                .Select(v => new ParkedVehicleSlimViewModel
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Type = v.Type,
                    Brand = v.Brand,
                    RegisteredAt = v.RegisteredAt,
                    Color = v.Color,
                }).ToList();

            if (selectedVehicleType is not null)
            {
                viewModel.ParkedVehicles = viewModel.ParkedVehicles.Where(m => m.Type == selectedVehicleType).ToList();
            }

            if (!string.IsNullOrEmpty(regNumber))
            {
                viewModel.ParkedVehicles = [viewModel.ParkedVehicles.FirstOrDefault(v => v.RegistrationNumber == regNumber)!];
            }
            
            if (alert is not null)
            {
                viewModel.Alert = alert;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _vehicleRepository.Find(v => v.Id == id);
            var model = result.Select(v => new ParkedVehicleViewModel
            {
                Id = v.Id,
                Brand = v.Brand,
                Color = v.Color,
                Model = v.Model,
                RegisteredAt = v.RegisteredAt,
                RegistrationNumber = v.RegistrationNumber,
                Type = v.Type,
                Wheels = v.Wheels
            }).First();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var garages = await _garageRepository.All();

            var viewModel = new CreateParkedVehicleViewModel
            {
                Garages = garages.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.ID.ToString()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateParkedVehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = new ParkedVehicle
                {
                    RegistrationNumber = viewModel.RegistrationNumber!,
                    Type = viewModel.Type,
                    Brand = viewModel.Brand!,
                    Model = viewModel.Model!,
                    Wheels = viewModel.Wheels,
                    RegisteredAt = viewModel.RegisteredAt,
                    Color = viewModel.Color
                };

                vehicle.RegisteredAt = DateTime.Now;

                var garage = (await _garageRepository.Find(x => x.ID == viewModel.GarageId)).Single();
                if (garage is not null)
                {
                    vehicle.GarageId = garage.ID;
                }

                var parkedVehicle = await _vehicleRepository.Create(vehicle);

                return RedirectToAction(nameof(Index), new
                {
                    IsActive = true,
                    Message = $"Successfully parked {parkedVehicle.Type} at {parkedVehicle.RegisteredAt} with registration number: {parkedVehicle.RegistrationNumber}",
                    Type = AlertType.Success
                });
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