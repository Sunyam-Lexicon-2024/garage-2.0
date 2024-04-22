using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage_2_0.Controllers
{
    public class VehicleController(
        IRepository<Garage> garageRepository,
        IRepository<ParkingSpot> parkingSpotRepository,
        IRepository<Vehicle> vehicleRepository) : Controller
    {
        private readonly IRepository<Garage> _garageRepository = garageRepository;
        private readonly IRepository<ParkingSpot> _parkingSpotRepository = parkingSpotRepository;
        private readonly IRepository<Vehicle> _vehicleRepository = vehicleRepository;

        public async Task<IActionResult> Index(AlertViewModel? alert, VehicleType? selectedVehicleType, string? regNumber)
        {
            var viewModel = new VehicleIndexViewModel();

            var vehicles = await _vehicleRepository.All();

            viewModel.ParkedVehicles = vehicles
                .OrderByDescending(v => v.RegisteredAt)
                .Select(v => new VehicleSlimViewModel
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Type = v.Type,
                    Brand = v.Brand,
                    RegisteredAt = v.RegisteredAt,
                    Color = v.Color,
                    ParkingSpotIds = AssembleSpotIdString([.. _parkingSpotRepository.GetManyToManyRelation(v.Id)])
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
            var model = result.Select(v => new VehicleViewModel
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

            var viewModel = new VehicleCreateViewModel
            {
                Garages = garages.Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle
                {
                    RegistrationNumber = viewModel.RegistrationNumber!,
                    Type = viewModel.Type,
                    Brand = viewModel.Brand!,
                    Model = viewModel.Model!,
                    Wheels = viewModel.Wheels,
                    Color = viewModel.Color
                };

                var garage = (await _garageRepository.Find(g => g.Id == viewModel.GarageId)).Single();
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
        public async Task<IActionResult> Edit(int id, Vehicle viewModel)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private static string AssembleSpotIdString(ICollection<ParkingSpot> spots)
        {
            return string.Join(", ", spots.Select(s => s.Id).ToArray());
        }
    }
}