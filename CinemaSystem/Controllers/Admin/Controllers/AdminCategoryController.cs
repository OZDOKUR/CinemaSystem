using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Controllers.Admin.Controllers
{

    [Authorize(Policy = "AdminOrWorkerRole")]
    public class AdminCategoryController : Controller
    {
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());

        public IActionResult Index()
        {
            var categoryall = _categoryManager.GetAll();

            var viewModel = new CategoryViewModel
            {
                CategoryList = categoryall,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddNewCategory(CategoryViewModel viewModel)
        {
            _categoryManager.Add(viewModel.Category);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult EditCategory(CategoryViewModel viewModel)
        {
            _categoryManager.Update(viewModel.Category);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            _categoryManager.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
