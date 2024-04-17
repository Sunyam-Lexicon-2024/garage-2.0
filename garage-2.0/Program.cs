var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddControllersWithViews(options => options.EnableEndpointRouting = false);

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
