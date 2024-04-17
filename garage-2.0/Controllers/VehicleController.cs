using Microsoft.AspNetCore.Mvc;

namespace garage_2._0.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
