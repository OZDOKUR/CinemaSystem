using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers
{
    public class AboutController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
