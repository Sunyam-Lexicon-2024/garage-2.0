using garage_2._0.Models;
using garage_2._0.ViewModels;
using Garage_2._0.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace garage_2._0.Controllers
{
    public class VehicleController(IRepository<ParkedVehicle> repository) : Controller
    {
        private IRepository<ParkedVehicle> _repository = repository;

        public async Task<IActionResult> Index()
        {
            var vehicles = await _repository.All();

            var model = vehicles.Select(x => new ParkedVehicleViewModel
            {
                Id = x.Id,
                RegistrationNumber = x.RegistrationNumber,
                Type = x.Type,
                Brand = x.Brand,
                Model = x.Model,
                Wheels = x.Wheels,
                RegisteredAt = x.RegisteredAt,
                Color = x.Color,
            }).ToList();

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
    }
}