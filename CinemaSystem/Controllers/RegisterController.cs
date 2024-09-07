using CinemaSystem.Models;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

       
        public RegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AnonymousOnly]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new RegisterViewModel
            {
                GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "male", Text = "Erkek" },
                    new SelectListItem { Value = "female", Text = "Kadın" },
                    new SelectListItem { Value = "other", Text = "Diğer" }
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel p)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    BirthDate = p.BirthDate,
                    Gender = p.Gender,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    PasswordHash = p.Password,
                    UserName = p.Email
                };

                var result = await _userManager.CreateAsync(user, p.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("HATA", item.Description);
                    }
                }
            }


            p.GenderOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "male", Text = "Erkek" },
                new SelectListItem { Value = "female", Text = "Kadın" },
                new SelectListItem { Value = "other", Text = "Diğer" }
            };

            return View(p);
        }
    }
}
