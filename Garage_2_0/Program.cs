using Garage_2_0.Models;
using Garage_2_0.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName
});

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddSingleton(configuration);

services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
services.AddDbContext<GarageDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

// Register repositories to DI container
services.AddTransient<IRepository<Garage>, GarageRepository>();
services.AddTransient<IRepository<ParkingSpot>, ParkingSpotRepository>();
services.AddTransient<IRepository<Vehicle>, VehicleRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMvc(config =>
{
    config.MapRoute(
        name: "default",
        template: "{controller=Vehicle}/{action=Index}/{id?}");
});

app.UseRouting();

app.Run();