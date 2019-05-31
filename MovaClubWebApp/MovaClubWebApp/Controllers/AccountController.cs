using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovaClubWebApp.Models.Account;
using MovaClubWebApp.MovaClubDb.Models;
namespace MovaClubWebApp.Controllers
{
    [Authorize(Roles = "Student, Teacher")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if (User.IsInRole("Teacher") || User.IsInRole("Student"))
            {
                return View();
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Account", new { area = "Admin" });
            }
            else if (User.IsInRole("Owner"))
            {
                return RedirectToAction("Index", "Account", new { area = "Owner" });
            }
            else
            {
                return View("Login");
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login( LoginModel model, string returnUrl = "/")
        {
            if (model.Provider == null)
            {
                if (model.Provider == "own")
                {
                    if (ModelState.IsValid)
                    {

                    }
                    else
                    {

                    }

                }
                else if (model.Provider == "google" || model.Provider == "facebook")
                {
                    return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl }, model.Provider);
                }
            }
           
            else
            {
                ModelState.AddModelError("", "Не выбран способ входа!");
            }
            return View(model);
        }

        public IActionResult Schedule()
        {
            return View("Schedule");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}