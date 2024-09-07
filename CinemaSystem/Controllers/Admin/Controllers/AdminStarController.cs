using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers.Admin.Controllers
{

    [Authorize(Policy = "AdminOrWorkerRole")]
    public class AdminStarController : Controller
    {
        private StarManager _starManager = new StarManager(new EfStarDal());
        public IActionResult Index()
        {
            var starList = _starManager.GetAll();
            var viewModel = new StarViewModel
            {
                StarList = starList
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNewStar(StarViewModel viewModel, IFormFile PictureFile)
        {
            if (PictureFile != null && PictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(PictureFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/stars/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PictureFile.CopyTo(stream);
                }

                viewModel.star.Picture = "/uploads/stars/" + fileName;
            }
            

            _starManager.Add(viewModel.star);
            return RedirectToAction("Index");
        }

       

        [HttpPost]
        public IActionResult EditStar(StarViewModel viewModel, IFormFile PictureFile, string ExistingPicture)
        {
            if (PictureFile != null && PictureFile.Length > 0)
            {
                var fileName = Path.GetFileName(PictureFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/stars/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PictureFile.CopyTo(stream);
                }

                viewModel.star.Picture = "/uploads/stars/" + fileName;
            }
            else
            {
                viewModel.star.Picture = ExistingPicture;
            }

            _starManager.Update(viewModel.star);
            return RedirectToAction("Index");
        }


        public IActionResult DeleteStar(int id)
        {

            _starManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
