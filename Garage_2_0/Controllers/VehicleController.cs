using Garage_2_0.Models;
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

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleRepository.All();

            var model = vehicles.Select(x => new ParkedVehicleSlimViewModel
            {
                Id = x.Id,
                RegistrationNumber = x.RegistrationNumber,
                Type = x.Type,
                Brand = x.Brand,
                RegisteredAt = x.RegisteredAt,
                Color = x.Color,
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _vehicleRepository.Find(v => v.Id == id);
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

        public async Task<IActionResult> Create()
        {
            var garages = await _garageRepository.All();

            var viewModel = new CreateParkedVehicleViewModel
            {
                Garages = garages.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
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
                    RegistrationNumber = viewModel.RegistrationNumber,
                    Type = viewModel.Type,
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    Wheels = viewModel.Wheels,
                    RegisteredAt = viewModel.RegisteredAt,
                    Color = viewModel.Color
                };

                vehicle.RegisteredAt = DateTime.Now;

                var result = await _garageRepository.Find(x => x.ID == viewModel.GarageId);
                var garage = result.FirstOrDefault();

                if (garage is not null)
                {
                    vehicle.GarageId = garage.ID;
                }

                await _vehicleRepository.Create(vehicle);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}