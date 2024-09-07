using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers.Admin.Controllers
{
    [Authorize(Policy = "AdminOrWorkerRole")]
    public class AdminUserController : Controller
    {
        private UserManager _userManager = new UserManager(new EfUserDal());
        private readonly UserManager<AppUser> _userRManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminUserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userRManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult User()
        {
            var user = _userManager.GetUsersByRoleId(3);
            return View(user);
        }

        public async Task<IActionResult> Worker()
        {
            var workers = await _userRManager.GetUsersInRoleAsync("Worker");
            var model = new WorkerViewModel
            {
                Users = workers.ToList(),
                NewWorker = new RegisterViewModel
                {
                    GenderOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "male", Text = "Erkek" },
                        new SelectListItem { Value = "female", Text = "Kadın" },
                        new SelectListItem { Value = "other", Text = "Diğer" }
                    }
                }
            };
            return View(model);
        }

        [Authorize(Policy = "AdminRole")]
        public IActionResult AddWorker()
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

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<IActionResult> AddWorker(RegisterViewModel p)
        {
            if (!ModelState.IsValid || p.Password != p.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler uyuşmuyor!");

                p.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "male", Text = "Erkek" },
                    new SelectListItem { Value = "female", Text = "Kadın" },
                    new SelectListItem { Value = "other", Text = "Diğer" }
                };

                return Json(new
                {
                    success = false,
                    errors = ModelState.ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }

            AppUser user = new AppUser
            {
                Name = p.Name,
                Surname = p.Surname,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                UserName = p.Email
            };

            var result = await _userRManager.CreateAsync(user, p.Password);

            if (result.Succeeded)
            {
                await _userRManager.AddToRoleAsync(user, "Worker");
                return Json(new { success = true });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Json(new
                {
                    success = false,
                    errors = ModelState.ToDictionary(
                        kv => kv.Key,
                        kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                });
            }
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var values = _userManager.GetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditUser(AppUser user)
        {
            // Kullanıcıyı güncelle
            _userManager.Update(user);

            // Kullanıcının rolünü kontrol et
            var roles = _userRManager.GetRolesAsync(user).Result; // İki durum da kontrol edilebilir: asenkron ve senkron.
            if (roles.Contains("Worker"))
            {
                return RedirectToAction("Worker");
            }

            return RedirectToAction("User");
        }


        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<IActionResult> DowngradeWorkerRole(int userId)
        {
            var user = await _userRManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return Json(new { success = false, errors = new[] { "Kullanıcı bulunamadı." } });
            }

            var currentRoles = await _userRManager.GetRolesAsync(user);

            foreach (var role in currentRoles)
            {
                await _userRManager.RemoveFromRoleAsync(user, role);
            }

            var roleResult = await _userRManager.AddToRoleAsync(user, "User");

            if (roleResult.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                var errors = roleResult.Errors.Select(e => e.Description).ToArray();
                return Json(new { success = false, errors = errors });
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminRole")]
        public async Task<IActionResult> UpdateUserRole(int userId)
        {
            var user = await _userRManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userRManager.GetRolesAsync(user);

            foreach (var role in currentRoles)
            {
                await _userRManager.RemoveFromRoleAsync(user, role);
            }

            var roleResult = await _userRManager.AddToRoleAsync(user, "Worker");

            if (roleResult.Succeeded)
            {
                return RedirectToAction("User");
            }
            else
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Error");
            }
        }
    }
}
