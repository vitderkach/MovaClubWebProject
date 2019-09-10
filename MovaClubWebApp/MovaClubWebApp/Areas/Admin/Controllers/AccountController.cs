using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovaClubWebApp.Areas.Admin.Models;
using MovaClubWebApp.Models.Shared;
using MovaClubWebApp.MovaClubDb;
using MovaClubWebApp.MovaClubDb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly MovaClubDbContext _context;

        public AccountController(ILogger<AccountController> logger, UserManager<AppUser> userManager, MovaClubDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {

            return View("Manage", new Models.ManageModel() { ViewUsers = await getUsers() });
        }


        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            return PartialView("UserTable", new Models.ManageModel() { ViewUsers = await getUsers() });
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                }
                else
                {
                    ModelState.AddModelError("User isn't deleted!", result.Errors.ElementAt(0).Description);
                }
            }
            else
            {
                ModelState.AddModelError("User doesn't found!", "The user doesn't found. Please, reload page or refer to a technical specialist.");
            }

            return PartialView("UserTable", new Models.ManageModel() { ViewUsers = await getUsers() });
        }



        [HttpGet]
        public async Task<IActionResult> AboutUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var viewUser = new ViewUser()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Birthday = user.BirthdayDate.ToString("dd.MM.yyyy"),
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    ChangeRoleRequest = (user.ChangeRoleRequest != null) ? user.ChangeRoleRequest : "Empty",
                    Role = (await _userManager.GetRolesAsync(user))[0]
                };
                return View("AboutUser", new Models.AboutUserModel() { ViewUser = viewUser });
            }
            else
            {
                ModelState.AddModelError("User doesn't found!", "The user doesn't found. Please, reload page or refer to a technical specialist.");
                return PartialView("UserTable", new Models.ManageModel() { ViewUsers = await getUsers() });
            }
        }


        private async Task<List<ViewUser>> getUsers()
        {
            var appUsers = await _context.Users.ToListAsync();
            List<ViewUser> viewUsers = new List<ViewUser>();
            foreach (var appUser in appUsers)
            {
                ViewUser viewUser = new ViewUser()
                {
                    FirstName = (appUser.FirstName != null) ? appUser.FirstName : "Empty",
                    LastName = (appUser.LastName != null) ? appUser.LastName : "Empty",
                    Birthday = (appUser.BirthdayDate != default(DateTime)) ? appUser.BirthdayDate.ToString("dd.MM.yyyy") : "Empty",
                    Email = (appUser.Email != null) ? appUser.Email : "Empty",
                    Gender = (appUser.Gender != null) ? appUser.Gender : "Empty",
                    PhoneNumber = (appUser.PhoneNumber != null) ? appUser.PhoneNumber : "Empty",
                    Role = ((await _userManager.GetRolesAsync(appUser)).ElementAt(0) != null) ? (await _userManager.GetRolesAsync(appUser)).ElementAt(0) : "Empty",
                    UserName = appUser.UserName


                };
                viewUsers.Add(viewUser);
            }

            return viewUsers;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeName(NameFieldModel model)
        {
            bool allFilled = true;
            string confirmText = null;
            if (model.FirstName == null)
            {
                ModelState.AddModelError("FirstName", "The field is required");
                allFilled = false;
            }
            if (model.LastName == null)
            {
                ModelState.AddModelError("LastName", "The field is required");
                allFilled = false;
            }

            if (allFilled == true)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is AppUser)
                {
                    if (user.FirstName == model.FirstName && user.LastName == model.LastName)
                    {
                        ModelState.AddModelError("", "New and old names match up");
                    }
                    else
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded == false)
                        {
                            for (int i = 0; i < result.Errors.Count(); i++)
                            {
                                ModelState.AddModelError("", result.Errors.ElementAt(i).Description);
                            }

                        }
                        else
                        {
                            confirmText = "Name successfully changed!";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
                }


            }
            var returnModel = await getUserProps(model.UserName);
            return PartialView("Settings/NameField", new NameFieldModel() { FirstName = returnModel.FirstName, LastName = returnModel.LastName, ContentOpened = true, ConfirmText = confirmText });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(RoleFieldModel model)
        {
            string confirmText = null;
            var returnModel = await getUserProps(model.UserName);
            return PartialView("Settings/NameField", new NameFieldModel() { FirstName = returnModel.FirstName, LastName = returnModel.LastName, ContentOpened = true, ConfirmText = confirmText });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmRole(string userName, string confirm)
        {
            string confirmText = null;
            string newRole = null;
            var user = await _userManager.FindByNameAsync(userName);
            if (user is AppUser)
            {
                bool boolConfirm;
                var parseResult = Boolean.TryParse(confirm, out boolConfirm);
                if (parseResult == true)
                {
                    if (boolConfirm == true)
                    {
                        await _userManager.RemoveFromRoleAsync(user, "Student");
                        await _userManager.RemoveFromRoleAsync(user, "Teacher");
                        await _userManager.RemoveFromRoleAsync(user, "Admin");
                        await _userManager.RemoveFromRoleAsync(user, "Owner");
                        await _userManager.AddToRoleAsync(user, user.ChangeRoleRequest);
                        newRole = user.ChangeRoleRequest;
                        confirmText = "The request sucessfully accepted";
                        user = await _userManager.FindByNameAsync(userName);
                    }
                    else
                    {

                        confirmText = "The request sucessfully cancelled";
                    }

                    user.ChangeRoleRequest = null;
                    var updateResult = await _userManager.UpdateAsync(user);
                }
                else
                {
                    ModelState.AddModelError("", "Confirm request is incorrect.");
                }

            }
            else
            {
                ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
            }

            var returnModel = await getUserProps(userName);
            return PartialView("Settings/RolerequestField", new RoleRequestFieldModel() { RoleRequest = ((returnModel.RoleRequest != null) ? returnModel.RoleRequest : "Empty"), ContentOpened = true, ConfirmText = confirmText, NewRole = newRole });
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(EmailFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (new EmailAddressAttribute().IsValid(model.Email))
            {
                if (model.Email != user.Email)
                {
                    if (!((await _userManager.FindByEmailAsync(model.Email)) is AppUser))
                    {

                        if (user is AppUser)
                        {
                            user.Email = model.Email;
                            user.EmailConfirmed = true;
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded == true)
                            {
                                confirmText = "The email was successfully changed";
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }

                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "The email is already busy");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Old and new email match up");
                }
            }
            else
            {
                ModelState.AddModelError("", "Input correct email adress");
            }
            var returnModel = await getUserProps(model.UserName);
            return PartialView("Settings/EmailField", new EmailFieldModel() { Email = user.Email, ContentOpened = true, ConfirmText = confirmText, UserName = model.UserName });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber(PhoneNumberFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (new PhoneAttribute().IsValid(model.PhoneNumber) && model.PhoneNumber.Length == 19)
            {
                if (user is AppUser)
                {
                    user.PhoneNumber = model.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        confirmText = "Phone number successfully changed!";
                    }
                    else
                    {
                        for (int i = 0; i < result.Errors.Count(); i++)
                        {
                            ModelState.AddModelError("", result.Errors.ElementAt(i).Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user doesn't found. Please, reload page or connect with a technical specialist.");
                }

            }
            else
            {
                ModelState.AddModelError("", "Input correct phone number");
            }


            var returnModel = await getUserProps(model.UserName);
            return PartialView("Settings/PhoneNumberField", new PhoneNumberFieldModel() { PhoneNumber = returnModel.PhoneNumber, ContentOpened = true, ConfirmText = confirmText });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeBirthday(BirthdayFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is AppUser)
            {
                DateTime birthdayDate;
                if (DateTime.TryParse(model.Birthday, out birthdayDate) && birthdayDate.Year < 1950 && birthdayDate <= DateTime.Now)
                {
                    user.BirthdayDate = birthdayDate;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded == true)
                    {
                        confirmText = "The email was successfully changed";
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }

                }
                else
                {
                    ModelState.AddModelError("", "The date is invalid");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user doesn't found. Please, reload page or connect with a technical specialist.");
            }



            var returnModel = await getUserProps(model.UserName);
            return PartialView("Settings/BirthdayField", new BirthdayFieldModel() { Birthday = returnModel.Birthday, ContentOpened = true, ConfirmText = confirmText, UserName = model.UserName });
        }

        private async Task<SettingsModel> getUserProps(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            SettingsModel model = null;
            if (user is AppUser)
            {
                model = new SettingsModel();
                model.FirstName = (user.FirstName != null) ? user.FirstName : "Empty";
                model.LastName = (user.LastName != null) ? user.LastName : "Empty";
                model.Email = (user.Email != null) ? user.Email : "Empty";
                model.Password = "\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022";
                model.Birthday = (user.BirthdayDate != default(DateTime)) ? user.BirthdayDate.ToString("yyyy-MM-dd") : "Empty";
                model.Gender = (user.Gender != null) ? user.Gender : "Empty";
                model.PhoneNumber = (user.PhoneNumber != null) ? user.PhoneNumber : "Empty";
                model.Role = (await _userManager.GetRolesAsync(user))[0];
                model.UserName = user.UserName;
            }
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            return View("Calendar");
        }
    }
}