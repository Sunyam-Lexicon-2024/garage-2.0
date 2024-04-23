using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels.Common;
using Garage_2_0.Models.ViewModels.Vehicle;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Garage_2_0.Controllers
{
    public class VehicleController(
        IConfiguration configuration,
        IRepository<Garage> garageRepository,
        IRepository<ParkedVehicle> vehicleRepository) : Controller
    {
        private readonly int _hourlyRate = configuration.GetValue<int>("GarageSettings:HourlyRate");
        private readonly IRepository<Garage> _garageRepository = garageRepository;
        private readonly IRepository<ParkedVehicle> _vehicleRepository = vehicleRepository;

        public async Task<IActionResult> Index(AlertViewModel? alert, VehicleType? selectedVehicleType, string? regNumber)
        {
            var viewModel = new IndexParkedVehicleViewModel();

            var vehicles = await _vehicleRepository.All();

            viewModel.ParkedVehicles = vehicles
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
                viewModel.ParkedVehicles = viewModel.ParkedVehicles.Where(m => m.Type == selectedVehicleType);
            }

            if (!string.IsNullOrEmpty(regNumber))
            {
                viewModel.ParkedVehicles = viewModel.ParkedVehicles.Where(m => m.RegistrationNumber!.Contains(regNumber));
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
            var viewModel = new CreateParkedVehicleViewModel();
            viewModel.Garages = await GetGarageSelectOptions();

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

                var garage = (await _garageRepository.Find(x => x.Id == viewModel.GarageId)).Single();
                if (garage is not null)
                {
                    vehicle.GarageId = garage.Id;
                }

                var parkedVehicle = await _vehicleRepository.Create(vehicle);

                return RedirectToAction(nameof(Index), new
                {
                    IsActive = true,
                    Message = $"Successfully parked {parkedVehicle.Type} at {parkedVehicle.RegisteredAt} with registration number: {parkedVehicle.RegistrationNumber}",
                    Type = AlertType.Success
                });
            }

            viewModel.Garages = await GetGarageSelectOptions();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var vehicle = (await _vehicleRepository.Find(v => v.Id == id)).Single();
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
                    await _vehicleRepository.Update(viewModel);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int id)
        {
            var removedVehicle = await _vehicleRepository.Delete(id);

            if (removedVehicle is not null)
            {
                var viewModel = new CheckoutVehicleViewModel
                {
                    CheckoutAt = DateTime.Now,
                    Vehicle = new ParkedVehicleSlimViewModel
                    {
                        Id = removedVehicle.Id,
                        RegisteredAt = removedVehicle.RegisteredAt,
                        RegistrationNumber = removedVehicle.RegistrationNumber,
                        Brand = removedVehicle.Brand,
                        Color = removedVehicle.Color,
                        Type = removedVehicle.Type,
                    },
                    ParkingPeriod = DateTime.Now - removedVehicle.RegisteredAt,
                    HourlyRate = _hourlyRate,
                    TotalParkingCost = (DateTime.Now - removedVehicle.RegisteredAt).Hours * _hourlyRate
                };

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DownloadCheckoutReceipt(CheckoutVehicleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string json = JsonSerializer.Serialize(viewModel);
            System.IO.File.WriteAllText(@"C:\", json);

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetGarageSelectOptions()
        {
            var garages = await _garageRepository.All();

            return garages.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}