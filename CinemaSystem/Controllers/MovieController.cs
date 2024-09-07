using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers
{
    public class MovieController : Controller
    {
        private MovieManager _movieManager = new MovieManager(new EfMovieDal());
        public IActionResult Description(string name)
        {

            var result = _movieManager.GetByName(name);
            return View("Description", result);
        }
    }
}
