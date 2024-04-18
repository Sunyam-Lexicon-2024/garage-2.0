using garage_2._0.Models;
using Microsoft.AspNetCore.Mvc;



namespace garage_2._0.Controllers
{
    public class VehicleController : Controller
    {
        private readonly GarageDbContext _context;

        public VehicleController(GarageDbContext context)
        {
            _context = context;
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNumber, Type, Brand, Model, Wheels, RegisteredAt, Color")] ParkedVehicle viewModel)
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

                _context.ParkedVehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Vehicle/Index
        public IActionResult Index()
        {
            var vehicles = _context.ParkedVehicles.ToList();
            return View(vehicles);
        }
    }
}