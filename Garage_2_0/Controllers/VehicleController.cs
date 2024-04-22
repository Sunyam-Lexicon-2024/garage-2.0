using Garage_2_0.Models;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Garage_2_0.Controllers
{
    public class VehicleController(IRepository<ParkedVehicle> repository) : Controller
    {
        private readonly IRepository<ParkedVehicle> _repository = repository;

        public async Task<IActionResult> Index()
        {
            var vehicles = await _repository.All();

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

                await _repository.Create(vehicle);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}