using Garage_2_0.Models;
using Garage_2_0.Models.Enums;
using Garage_2_0.Models.ViewModels;
using Garage_2_0.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Garage_2._0.Web.Controllers;

public class StatsController(ILogger<StatsController> logger,
                            IRepository<ParkedVehicle> vehicleRepository
                            ) : Controller
{
    private readonly IRepository<ParkedVehicle> _vehicleRepository = vehicleRepository;

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

    private static Dictionary<VehicleType, int> CalculateVehiclesRegistered(IEnumerable<ParkedVehicle> vehicles)
    {
        Dictionary<VehicleType, int> vehicleCountList = new() {
            {VehicleType.Car, vehicles.Where(v => v.Type == VehicleType.Car).ToList().Count},
            {VehicleType.Motorcycle,  vehicles.Where(v => v.Type == VehicleType.Motorcycle).ToList().Count},
            {VehicleType.Bus,  vehicles.Where(v => v.Type == VehicleType.Bus).ToList().Count},
            {VehicleType.Boat,  vehicles.Where(v => v.Type == VehicleType.Boat).ToList().Count},
            {VehicleType.Airplane,  vehicles.Where(v => v.Type == VehicleType.Airplane).ToList().Count},
        };

        return vehicleCountList;
    }

    private static int CalculateTotalRevenue(IEnumerable<ParkedVehicle> vehicles)
    {
        int total = 0;
        int rate = 5; // where to place?
        foreach (var v in vehicles)
        {
            var hours = (DateTime.Now - v.RegisteredAt).Hours;
            total += hours * rate;
        }
        return total;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
