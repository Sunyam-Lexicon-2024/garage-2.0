using Garage_2_0.Models;
using Garage_2_0.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
services.AddDbContext<GarageDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

// Register repositories to DI container
services.AddTransient<IRepository<ParkedVehicle>, ParkedVehicleRepository>();
services.AddTransient<IRepository<Garage>, GarageRepository>();

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