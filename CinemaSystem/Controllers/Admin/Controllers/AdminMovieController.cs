using Business.Abstract;
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
    public class AdminMovieController : Controller
    {
        private MovieManager _movieManager = new MovieManager(new EfMovieDal());
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        private StarManager _starManager = new StarManager(new EfStarDal());

        public IActionResult Index()
        {
            var movies = _movieManager.GetAll();
            var categories = _categoryManager.GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            var stars = _starManager.GetAll().Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();

            var model = new MovieViewModel
            {
                MovieList = movies,
                Categories = categories,
                Stars = stars
            };

            return View(model);
        }

       

        [HttpPost]
        public IActionResult AddNewMovie(MovieViewModel model, int[] selectedStars, IFormFile PictureFile, IFormFile DirectorPicture, IFormFile Picture2File)
        {
            if (PictureFile != null && PictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(PictureFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/movies/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PictureFile.CopyTo(stream);
                }

                model.Movie.Picture = "/uploads/movies/" + fileName;
            }

            if (DirectorPicture != null && DirectorPicture.Length > 0)
            {
                var fileName = Path.GetFileName(DirectorPicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/directors/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DirectorPicture.CopyTo(stream);
                }

                model.Movie.DirectorPicture = "/uploads/directors/" + fileName;
            }
            if (Picture2File != null && Picture2File.Length > 0)
            {
                var fileName = Path.GetFileName(Picture2File.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/movies/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Picture2File.CopyTo(stream);
                }

                model.Movie.Background = "/uploads/movies/" + fileName;
            }

            foreach (var starId in selectedStars)
            {
                model.Movie.MovieStars.Add(new MovieStar { StarId = starId });
            }

            _movieManager.Add(model.Movie);
            return RedirectToAction("Index");
        }

        public IActionResult EditMovie(int id)
        {
            var movie = _movieManager.GetById(id);
            var categories = _categoryManager.GetAll();
            var stars = _starManager.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Stars = new SelectList(stars, "Id", "Name");
            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie movie, IFormFile PictureFile, IFormFile DirectorPicture, IFormFile BackgroundFile,
    string ExistingPicture, string ExistingDirectorPicture, string ExistingBackground)
        {
            if (PictureFile != null && PictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(PictureFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/movies/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PictureFile.CopyTo(stream);
                }

                movie.Picture = "/uploads/movies/" + fileName;
            }
            else
            {
                movie.Picture = ExistingPicture;
            }

            if (DirectorPicture != null && DirectorPicture.Length > 0)
            {
                var fileName = Path.GetFileName(DirectorPicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/directors/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DirectorPicture.CopyTo(stream);
                }

                movie.DirectorPicture = "/uploads/directors/" + fileName;
            }
            else
            {
                movie.DirectorPicture = ExistingDirectorPicture;
            }

            if (BackgroundFile != null && BackgroundFile.Length > 0)
            {
                var fileName = Path.GetFileName(BackgroundFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/movies/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    BackgroundFile.CopyTo(stream);
                }

                movie.Background = "/uploads/movies/" + fileName;
            }
            else
            {
                movie.Background = ExistingBackground;
            }

            _movieManager.Update(movie);
            return RedirectToAction("Index");
        }


        public ActionResult DeleteMovie(int id)
        {
            _movieManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}


