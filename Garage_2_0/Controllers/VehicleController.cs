using Garage_2_0.Models;
using Garage_2_0.Models.Configurations;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels.Common;
using Garage_2_0.Models.ViewModels.Vehicle;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage_2_0.Controllers
{
    public class VehicleController(
        IConfiguration configuration,
        ILogger<VehicleController> logger,
        IRepository<Garage> garageRepository,
        IRepository<ParkingSpot> parkingSpotRepository,
        IRepository<Vehicle> vehicleRepository) : Controller
    {
        private readonly ILogger _logger = logger;
        private readonly int _hourlyRate = configuration.GetValue<int>("GarageSettings:HourlyRate");
        private readonly IRepository<Garage> _garageRepository = garageRepository;
        private readonly IRepository<ParkingSpot> _parkingSpotRepository = parkingSpotRepository;
        private readonly IRepository<Vehicle> _vehicleRepository = vehicleRepository;
        private readonly SpotAllocationRules _spotAllocationRules = new();

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

            var viewModel = new VehicleIndexViewModel
            {
                ParkedVehicles = vehicles
                    .Select(v => new VehicleSlimViewModel
                    {
                        Id = v.Id,
                        RegistrationNumber = v.RegistrationNumber,
                        Type = v.Type,
                        Brand = v.Brand,
                        RegisteredAt = v.RegisteredAt,
                        Color = v.Color,
                        ParkingSpotIds = AssembleSpotIdString([.. _parkingSpotRepository.GetManyToManyRelation(v.Id)])
                    }).ToList()
            };

            if (alert is not null)
            {
                viewModel.Alert = alert;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var vehicle = (await _vehicleRepository.Find(v => v.Id == id)).Single();
            if (vehicle is null)
            {
                _logger.LogError("{Message}", $"Could not find vehicle with ID [{id}]");
                return NotFound();
            }

            VehicleViewModel viewModel = new()
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                Model = vehicle.Model,
                RegisteredAt = vehicle.RegisteredAt,
                RegistrationNumber = vehicle.RegistrationNumber,
                Type = vehicle.Type,
                Wheels = vehicle.Wheels
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Create()
        {
            VehicleCreateViewModel viewModel = new()
            {
                Garages = await GetGarageSelectOptions()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel viewModel)
        {
            int maxOccupied = _spotAllocationRules.Collection[viewModel.Type].MaxOccupied;
            int requiredSpotCount = _spotAllocationRules.Collection[viewModel.Type].RequiredSpotCount;

            var garageSpots = await _parkingSpotRepository.Find(s => s.GarageId == viewModel.GarageId);
            var freeSpots = GetSpots(garageSpots, maxOccupied, requiredSpotCount, viewModel.Type);

            if (!ValidSpotSequence(freeSpots))
            {
                ModelState.AddModelError("InvalidSpotSequence", $"Not enough spots for vehicle type (Required amount: {requiredSpotCount})");
            }

            if (freeSpots.Count() < requiredSpotCount)
            {
                ModelState.AddModelError("GarageFull", "Garage is full");
            }

            if (ModelState.IsValid)
            {
                if (await _vehicleRepository.Any(v => v.RegistrationNumber == viewModel.RegistrationNumber))
                {
                    ModelState.AddModelError("RegistrationNumber", $"Vehicle with {viewModel.RegistrationNumber} already in garage");
                    return View(viewModel);
                }

                Vehicle vehicle = new()
                {
                    RegistrationNumber = viewModel.RegistrationNumber!,
                    Type = viewModel.Type,
                    Brand = viewModel.Brand!,
                    Model = viewModel.Model!,
                    Wheels = viewModel.Wheels,
                    Color = viewModel.Color
                };

                foreach (var spot in freeSpots)
                {
                    spot.ContainsVehicleType = vehicle.Type;
                    spot.Vehicles.Add(vehicle);
                    vehicle.ParkingSpots.Add(spot);
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

            VehicleCreateViewModel viewModel = new()
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
        public async Task<IActionResult> Edit(int id, VehicleCreateViewModel viewModel)
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
                    Vehicle = new VehicleSlimViewModel
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id is null)
            {
                _logger.LogError("{Message}", $"garage with ID [{id}] not found");
                return NotFound();
            }
            else
            {
                var relatedSpots = _parkingSpotRepository.GetManyToManyRelation((int)id).ToList();
                var vehicleToDelete = await _vehicleRepository.Delete((int)id);
                foreach (var relatedSpot in relatedSpots)
                {
                    relatedSpot.ContainsVehicleType = null;
                    await _parkingSpotRepository.Update(relatedSpot);
                }
                return RedirectToAction(nameof(Index));
            }
        }

        private static string AssembleSpotIdString(ICollection<ParkingSpot> spots)
        {
            return string.Join(", ", spots.Select(s => s.Id).ToArray());
        }

        private IEnumerable<ParkingSpot> GetSpots(IEnumerable<ParkingSpot> garageSpots,
                                          int maxOccupied,
                                          int requiredSpotCount,
                                          VehicleType vehicleType)
        {
            return garageSpots
                .Where(spot => _vehicleRepository.GetManyToManyRelation(spot.Id).Count() <= maxOccupied)
                .Where(spot => spot.ContainsVehicleType == vehicleType || spot.ContainsVehicleType == null)
                .Take(requiredSpotCount);
        }

        private static bool ValidSpotSequence(IEnumerable<ParkingSpot> freeSpots)
        {
            for (int i = 0; i < freeSpots.Count() - 1; i++)
            {
                if (freeSpots.ElementAt(i + 1).Id - freeSpots.ElementAt(i).Id != 1)
                {
                    return false;
                }
            }
            return true;
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
