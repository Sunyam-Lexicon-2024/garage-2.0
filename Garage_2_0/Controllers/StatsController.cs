using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Garage_2._0.Web.Controllers;

public class StatsController(IConfiguration configuration,
                            ILogger<StatsController> logger,
                            IRepository<Vehicle> vehicleRepository
                            ) : Controller
{
    private readonly IRepository<Vehicle> _vehicleRepository = vehicleRepository;
    private readonly int _hourlyRate = configuration.GetValue<int>("GarageSettings:HourlyRate");
    private readonly ILogger<StatsController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var viewModel = new StatsViewModel();

        var vehicles = await _vehicleRepository.All();

        viewModel.TotalWheelCount = vehicles.Select(x => x.Wheels).Sum();
        viewModel.AccumulatedRevenue = CalculateTotalRevenue(vehicles);
        viewModel.VehicleCountList = CalculateVehiclesRegistered(vehicles);

        return View(viewModel);
    }

    private static Dictionary<VehicleType, int> CalculateVehiclesRegistered(IEnumerable<Vehicle> vehicles)
    {
        Dictionary<VehicleType, int> vehicleCountList = new() {
            {VehicleType.Car, vehicles.Where(v => v.Type == VehicleType.Car).Count()},
            {VehicleType.Motorcycle,  vehicles.Where(v => v.Type == VehicleType.Motorcycle).Count()},
            {VehicleType.Bus,  vehicles.Where(v => v.Type == VehicleType.Bus).Count()},
            {VehicleType.Boat,  vehicles.Where(v => v.Type == VehicleType.Boat).Count()},
            {VehicleType.Airplane,  vehicles.Where(v => v.Type == VehicleType.Airplane).Count()},
        };

        return vehicleCountList;
    }

    private int CalculateTotalRevenue(IEnumerable<Vehicle> vehicles)
    {
        int total = 0;
        foreach (var v in vehicles)
        {
            var hours = (DateTime.Now - v.RegisteredAt).Hours;
            total += hours * _hourlyRate;
        }
        return total;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
