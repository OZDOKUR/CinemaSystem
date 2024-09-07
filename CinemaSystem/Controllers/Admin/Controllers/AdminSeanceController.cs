using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaSystem.Controllers.Admin.Controllers
{

    [Authorize(Policy = "AdminOrWorkerRole")]
    public class AdminSeanceController : Controller
    {

        SeanceManager _seanceManager = new SeanceManager(new EfSeanceDal());
        MovieManager _movieManager = new MovieManager(new EfMovieDal());
        MovieTheaterManager _theaterManager = new MovieTheaterManager(new EfMovieTheaterDal());
        public IActionResult Index()
        {
            var seance = _seanceManager.GetAllT();
            return View(seance);
        }

        [HttpGet]
        public IActionResult AddNewSeance()
        {
            var movies = _movieManager.GetAll();
            ViewBag.Movies = new SelectList(movies, "Id", "Name");
            var theaters = _theaterManager.GetAll();
            ViewBag.theaters = new SelectList(theaters, "Id", "Name");

            var addSeanceViewModel = new Seance
            {
                Date = DateTime.Now,
                Movie = new Movie(),
                MovieTheater = new MovieTheater(),

            };

            return View(addSeanceViewModel);
        }

        [HttpPost]
        public IActionResult AddNewSeance(Seance addSeanceViewModel)
        {
            addSeanceViewModel.SeatsOccupied = "X";
            if (addSeanceViewModel.Date < DateTime.Now)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _seanceManager.Add(addSeanceViewModel);
                return RedirectToAction("Index");
            }
            
            
        }

        public IActionResult EditSeance(int id)
        {
            var seance = _seanceManager.GetById(id);
            if (seance == null)
            {
                return NotFound();
            }

            var movies = _movieManager.GetAll();
            ViewBag.Movies = new SelectList(movies, "Id", "Name");

            var theaters = _theaterManager.GetAll();
            ViewBag.Theaters = new SelectList(theaters, "Id", "Name");

            return View(seance);
        }

        [HttpPost]
        public IActionResult EditSeance(Seance seance)
        {
            var deneme = seance;
            if (seance.Date < DateTime.Now)
            {
                seance.Status = false;
            }
            else
            {
                seance.Status = true;
            }
            _seanceManager.Update(seance);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSeance(int id)
        {
            _seanceManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
