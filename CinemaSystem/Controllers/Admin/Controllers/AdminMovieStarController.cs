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
    public class AdminMovieStarController : Controller
    {

        MovieManager _movieManager = new MovieManager(new EfMovieDal());
        StarManager _starManager = new StarManager(new EfStarDal());
        MovieStarManager _movieStarManager = new MovieStarManager(new EfMovieStarDal());
        public IActionResult Index()
        {
            var movies = _movieManager.GetAll();
            var movieStars = _movieStarManager.GetAll();

            var movieStarViewModels = movies.Select(movie => new MovieStarViewModel
            {
                MovieName = movie.Name,
                StarNames = movieStars
                            .Where(ms => ms.MovieId == movie.Id && ms.Star != null)
                            .Select(ms => ms.Star.Name)
                            .ToList()
            }).ToList();

            return View(movieStarViewModels);
        }
        [HttpGet]
        public IActionResult AddNewMovieStar()
        {
            var movies = _movieManager.GetAll();
            var stars = _starManager.GetAll();

            var viewModel = new AddMovieStarViewModel
            {
                Movies = movies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),
                Stars = stars.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList(),
                ExistingMovieStars = new List<MovieStar>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNewMovieStar(AddMovieStarViewModel viewModel)
        {

            var movieStar = new MovieStar
            {
                MovieId = viewModel.MovieId,
                StarId = viewModel.StarId
            };

            _movieStarManager.Add(movieStar);

            var movies = _movieManager.GetAll();
            var stars = _starManager.GetAll();

            viewModel.Movies = movies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
            viewModel.Stars = stars.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
            viewModel.ExistingMovieStars = _movieStarManager.GetAll().Where(ms => ms.MovieId == viewModel.MovieId).ToList();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveMovieStar(string movieName)
        {
            var movie = _movieManager.GetAll().FirstOrDefault(m => m.Name == movieName);

            if (movie == null)
            {
                return NotFound();
            }

            var movieStars = _movieStarManager.GetAll().Where(ms => ms.MovieId == movie.Id).ToList();
            var stars = _starManager.GetAll();

            var viewModel = new RemoveMovieStarViewModel
            {
                Movie = movie,
                MovieStars = movieStars,
                Stars = stars
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RemoveMovieStar(string movieName, string starName)
        {

            var movie = _movieManager.GetAll().FirstOrDefault(m => m.Name == movieName);
            var star = _starManager.GetAll().FirstOrDefault(s => s.Name == starName);

            if (movie == null || star == null)
            {
                return NotFound();
            }

            var movieStar = _movieStarManager.GetAll()
                .FirstOrDefault(ms => ms.MovieId == movie.Id && ms.StarId == star.Id);

            if (movieStar != null)
            {
                _movieStarManager.Delete(movieStar);
            }

            return RedirectToAction("RemoveMovieStar", new { movieName });
        }

    }
}
