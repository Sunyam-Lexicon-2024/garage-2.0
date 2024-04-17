using garage_2._0.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
services.AddDbContext<GarageDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMvc(config =>
{
    config.MapRoute(
        name: "default",
        template: "{controller=Vehicle}/{action=List}/{id?}");
});

app.UseRouting();

app.Run();