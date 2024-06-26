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
            var vehicles = await _vehicleRepository.All();

            if (selectedVehicleType is not null)
            {
                vehicles = vehicles.Where(m => m.Type == selectedVehicleType);
            }

            if (!string.IsNullOrEmpty(regNumber))
            {
                vehicles = vehicles.Where(m => m.RegistrationNumber!.Contains(regNumber));
            }

            var viewModel = new IndexParkedVehicleViewModel();

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

            viewModel.Alert = alert;

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
            viewModel.Garages = await GetGarageSelectOptions();

            if (ModelState.IsValid)
            {
                if (await _vehicleRepository.Any(v => v.RegistrationNumber == viewModel.RegistrationNumber))
                {
                    ModelState.AddModelError("RegistrationNumber", "Registreringsnumret finns redan, v�nligen ange ett annat nummer.");
                    return View(viewModel);
                }
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

                var garage = (await _garageRepository.Find(g => g.Id == viewModel.GarageId)).Single();
                if (garage is not null)
                {
                    vehicle.GarageId = garage.Id;
                    vehicle.Garage = garage;
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

            var vehicle = (await _vehicleRepository.Find(v => v.Id == id)).Single();

            if (vehicle == null)
            {
                return NotFound();
            }

            CreateParkedVehicleViewModel viewModel = new()
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                RegisteredAt = vehicle.RegisteredAt,
                Type = vehicle.Type,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Wheels = vehicle.Wheels,
                Color = vehicle.Color,
                GarageId = vehicle.GarageId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateParkedVehicleViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var vehicleToUpdate = (await _vehicleRepository.Find(v => v.Id == viewModel.Id)).Single();

                    vehicleToUpdate.Color = viewModel.Color;
                    vehicleToUpdate.Brand = viewModel.Brand ?? "unknown";
                    vehicleToUpdate.Model = viewModel.Model ?? "unknown";
                    vehicleToUpdate.Wheels = viewModel.Wheels;

                    await _vehicleRepository.Update(vehicleToUpdate);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
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

            return garages.Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString()
            }).ToList();
        }
    }
}
