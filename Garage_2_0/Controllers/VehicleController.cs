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
        IRepository<ParkedVehicle> vehicleRepository) : Controller
    {
        private readonly IRepository<Garage> _garageRepository = garageRepository;
        private readonly IRepository<ParkedVehicle> _vehicleRepository = vehicleRepository;

        public async Task<IActionResult> Index(AlertViewModel? alert)
        {
            var model = new IndexParkedVehicleViewModel();

            var vehicles = await _vehicleRepository.All();
            model.ParkedVehicles = vehicles
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

            if (alert is not null)
            {
                model.Alert = alert;
            }

            return View(model);
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
    }
}