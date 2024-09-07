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
    public class AdminMovieTheaterController : Controller
    {
        private MovieTheaterManager _movieTheaterManager = new MovieTheaterManager(new EfMovieTheaterDal());

        private List<string> _cities = new List<string>
        {
            "Adana", "Adıyaman", "Afyonkarahisar", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balıkesir",
            "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli",
            "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkâri",
            "Hatay", "Isparta", "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir",
            "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir",
            "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat",
            "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman",
            "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"
        };

        public IActionResult Index()
        {
            var theaters = _movieTheaterManager.GetAll();
            var citiesSelectList = _cities.Select(c => new SelectListItem { Value = c, Text = c }).ToList();

            var viewModel = new MovieTheaterViewModel
            {
                MovieTheaterList = theaters,
                Cities = new SelectList(citiesSelectList, "Value", "Text")
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNewTheater(MovieTheaterViewModel viewModel)
        {
            _movieTheaterManager.Add(viewModel.MovieTheater);
            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public IActionResult EditTheater(MovieTheaterViewModel viewModel)
        {
            _movieTheaterManager.Update(viewModel.MovieTheater);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTheater(int id)
        {
            _movieTheaterManager.Delete(id);
            return RedirectToAction("Index");
        }
    
        
    }
}
