using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace CinemaSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieManager _movieManager = new MovieManager(new EfMovieDal());
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var movies = _movieManager.GetAll();
            var categories = _categoryManager.GetAll();

            var viewModel = new DashboardViewModel
            {
                categories = categories,
                movies = movies,
            };
            return View(viewModel);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
