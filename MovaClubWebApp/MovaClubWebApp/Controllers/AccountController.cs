using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovaClubWebApp.Models.Account;
using MovaClubWebApp.Models.Shared;
using MovaClubWebApp.MovaClubDb;
using MovaClubWebApp.MovaClubDb.Models;
using MovaClubWebApp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
namespace MovaClubWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly MovaClubDbContext _context;
        public AccountController(SignInManager<AppUser> signInManager, ILogger<AccountController> logger,
            IAuthenticationSchemeProvider schemeProvider, UserManager<AppUser> userManager, IEmailSender emailSender, MovaClubDbContext context)
        {
            _signInManager = signInManager;
            _schemeProvider = schemeProvider;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {

                if (User.IsInRole("Teacher") || User.IsInRole("Student"))
                {
                    return View("Index");
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
                    return Redirect("/Account/Login");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {

            if (_signInManager.IsSignedIn(User) || HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View("Login", new LoginModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = "/")
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Identificator);
                if (user != null)
                {
                    var checkPass = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (checkPass)
                    {
                        if (user.EmailConfirmed)
                        {
                            var result = await _signInManager.PasswordSignInAsync(model.Identificator, model.Password, model.RememberMe, false);
                            if (result.Succeeded)
                            {
                                await SetUserCookies(user.Id);
                                if ((!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) && returnUrl != "/")
                                {
                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    try
                                    {
                                        return RedirectToAction("Index");
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }

                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "The email isn't confirmed!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong username and (or) password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username and (or) password");
                }
            }
            else
            {
                return View(model);
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register", new RegisterModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user is AppUser)
                {
                    ModelState.AddModelError("UserName", "The name already busy!");
                    return View("Register", model);
                }

                user = await _userManager.FindByEmailAsync(model.Email);
                if (user is AppUser)
                {
                    ModelState.AddModelError("Email", "The email already busy!");
                    return View("Register", model);
                }
                user = new AppUser() { Email = model.Email, BirthdayDate = (DateTime)model.BirthdayDate, FirstName = model.FirstName, LastName = model.LastName, UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.UserType == UserType.Teacher)
                    {
                        await _userManager.AddToRoleAsync(user, "Teacher");

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                    }

                    await ConfirmEmail(model.Email);
                    return View("Verify", new VerifyModel { Email = model.Email });
                }
                else
                {
                    for (int i = 0; i < result.Errors.Count(); i++)
                    {
                        ModelState.AddModelError("", result.Errors.ElementAt(i).Description);
                    }
                }

            }
            return View("Register", model);

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmChangeEmail([FromQuery] string userId, [FromQuery] string newEmail, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            string confirmText = null;

            if (user is AppUser)
            {
                var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

                if (result.Succeeded)
                {
                    confirmText = "The email successfully changed.";
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

            }

            return View("Confirm", new ConfirmModel() { ConfirmText = confirmText });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmNewEmail([FromQuery] string userId, [FromQuery] string code)
        {

            var user = await _userManager.FindByIdAsync(userId);
            string confirmText = null;
            if (user is AppUser)
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    confirmText = "The email successfully confirmed.";
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

            }

            return View("Confirm", new ConfirmModel() { ConfirmText = "Thank you for registrating!" });
        }


        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var model = await getUserProps(User.Identity.Name);
            return View("Settings", model);
        }

        [HttpGet("{provider}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string provider, string returnUrl = "/")
        {
            if (_signInManager.IsSignedIn(User) || HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            var schemes = await _schemeProvider.GetAllSchemesAsync();
            var providerScheme = schemes.FirstOrDefault(scheme => scheme.Name == provider);
            if (!(!string.IsNullOrEmpty(provider) && providerScheme is AuthenticationScheme))
            {
                ModelState.AddModelError("", "You must login firstly!");
                return View("Login");
            }
            if (!(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) || returnUrl == "/")
            {
                returnUrl = Url.Action("Index", "Account");

            }
            var redirectUri = Url.Action("ExternalLogin", "Account", new { returnUrl });
            return Challenge(new AuthenticationProperties() { RedirectUri = redirectUri }, provider);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(TemporaryAuthenticationDefaults.AuthenticationScheme);
            if (authenticateResult.Succeeded)
            {
                var id = authenticateResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                string firstName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName);
                string surName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Surname);
                string email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
                string provider = authenticateResult.Principal.Identity.AuthenticationType;
                string birthday = authenticateResult.Principal.FindFirstValue(ClaimTypes.DateOfBirth);
                string gender = authenticateResult.Principal.FindFirstValue(ClaimTypes.Gender);
                if (provider == "Google")
                {
                    string accessToken = HttpContext.GetTokenAsync(TemporaryAuthenticationDefaults.AuthenticationScheme, "access_token").Result;
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://people.googleapis.com/v1/people/");
                    var response = await client.GetAsync($"me?personFields=birthdays,genders&fields=birthdays(date,text),genders/value&access_token={accessToken}");
                    if (!response.IsSuccessStatusCode)
                    {
                        var json = Json(new { result = response.StatusCode });
                        return json;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        dynamic peopleService = JObject.Parse(content);
                        gender = peopleService.genders[0].value;
                        birthday = peopleService.birthdays[0].date.day + "." + peopleService.birthdays[0].date.month + "." + peopleService.birthdays[0].date.year;
                    }
                }
                else
                {
                    birthday = authenticateResult.Principal.FindFirstValue(ClaimTypes.DateOfBirth);
                    gender = authenticateResult.Principal.FindFirstValue(ClaimTypes.Gender);
                }
                var user = await _userManager.FindByLoginAsync(provider, id);
                if (user is AppUser)
                {
                    await _signInManager.SignInAsync(user, true, null);
                }
                else
                {
                    user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new AppUser() { Email = email, EmailConfirmed = true, BirthdayDate = DateTime.ParseExact(birthday, "MM/dd/yyyy", CultureInfo.InvariantCulture), FirstName = firstName, LastName = surName, Gender = gender, UserName = provider[0] + "-" + id };
                        await _userManager.CreateAsync(user);
                        await _userManager.AddToRoleAsync(user, "Student");
                    }

                    await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, id, provider));
                    await _signInManager.SignInAsync(user, true, null);
                    await SetUserCookies(user.Id);
                }

            }
            return Redirect(returnUrl);
        }


        public IActionResult Schedule()
        {
            return View("Schedule");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        [ActionName("Verify")]
        public async Task<IActionResult> Verify_Post(VerifyModel model)
        {
            await ConfirmEmail(model.Email);
            return View("Verify", model);
        }

        private async Task ConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is AppUser && user.EmailConfirmed == false)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmNewEmail",
                    "Account",
                    new { userId = user.Id, code = token },
                    protocol: Request.Scheme);
                await SendEmail(email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }

        }

        private async Task SendEmail(string email, string subject, string htmlMessage)
        {
            await _emailSender.SendEmailAsync(email, subject, htmlMessage);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordFieldModel model)
        {
            bool allFilled = true;
            string confirmText = null;
            if (model.NewPassword == null)
            {
                ModelState.AddModelError("NewPassword", "The field is required");
                allFilled = false;
            }
            if (model.ConfirmPassword == null)
            {
                ModelState.AddModelError("ConfirmPassword", "The field is required");
                allFilled = false;
            }
            if (model.OldPassword == null)
            {
                ModelState.AddModelError("OldPassword", "The field is required");
                allFilled = false;
            }
            if (allFilled == true)
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "new password and confirm password must coincide!");
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    if (user is AppUser)
                    {
                        var passwordMatch = model.OldPassword == model.NewPassword;
                        if (passwordMatch == true)
                        {
                            ModelState.AddModelError("", "New and old passwords match up");
                        }
                        else
                        {

                            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                            if (result.Succeeded == false)
                            {
                                for (int i = 0; i < result.Errors.Count(); i++)
                                {
                                    ModelState.AddModelError("", result.Errors.ElementAt(i).Description);
                                }

                            }
                            else
                            {
                                confirmText = "Password successfully changed!";
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
                    }
                }
            }
            var returnModel = await getUserProps(User.Identity.Name);
            return PartialView("Settings/PasswordField", new PasswordFieldModel() { VisiblePassword = "\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022", ContentOpened = true, ConfirmText = confirmText });

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
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
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
            var returnModel = await getUserProps(User.Identity.Name);
            return PartialView("Settings/NameField", new NameFieldModel() { FirstName = returnModel.FirstName, LastName = returnModel.LastName, ContentOpened = true, ConfirmText = confirmText });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(RoleFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = Enum.Parse(typeof(UserType), model.Role);
            if (user != null)
            {
                if (_context.Roles.SingleOrDefault(r => r.Name == role.ToString()) != null)
                {
                    user.ChangeRoleRequest = role.ToString();
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        confirmText = "The request successfuly added.";
                    }
                    else
                    {
                        ModelState.AddModelError("", "The request doesn't added. Please, reload page or refer to administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The role doesn't exist.");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
            }
            var returnModel = await getUserProps(User.Identity.Name);
            return PartialView("Settings/RoleField", new RoleFieldModel()
            {
                Role = ((await _userManager.GetRolesAsync(user))[0]),
                ContentOpened = true,
                ConfirmText = confirmText,
                RoleRequest = (user.ChangeRoleRequest != null) ? user.ChangeRoleRequest : "Empty"
            });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(EmailFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (new EmailAddressAttribute().IsValid(model.Email))
            {
                if (model.Email != user.Email)
                {
                    if (!((await _userManager.FindByEmailAsync(model.Email)) is AppUser))
                    {

                        if (user is AppUser)
                        {
                            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);

                            var resetUrl = Url.Action(
            "ConfirmChangeEmail",
            "Account",
            new { userId = user.Id, newEmail = model.Email, token = token },
         protocol: Request.Scheme);
                            await SendEmail(model.Email, "Email change request", $"Please confirm your new email by <a href='{HtmlEncoder.Default.Encode(resetUrl)}'>clicking here</a>." +
                                $"If you didn't do it, please ignore the message.");
                            confirmText = $"We send you you a message by {user.Email} with request to change to new email. Please check your email.";
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
            var returnModel = await getUserProps(User.Identity.Name);
            return PartialView("Settings/EmailField", new EmailFieldModel() { Email = user.Email, ContentOpened = true, ConfirmText = confirmText });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber(PhoneNumberFieldModel model)
        {
            string confirmText = null;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.PhoneNumber != null)
            {
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
                        ModelState.AddModelError("", "The user doesn't found. Please, reload page or refer to administrator.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Input correct phone number");
                }
            }
            else
            {
                ModelState.AddModelError("PhoneNumber", "The field is required");
            }


            var returnModel = await getUserProps(User.Identity.Name);
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
                model.RoleRequest = (user.ChangeRoleRequest != null) ? user.ChangeRoleRequest : "Empty";
            }
            return model;
        }

        private async Task SetUserCookies(string userId)
        {
            int loginCount = 0;
            int.TryParse(HttpContext.Request.Cookies[$"LOGIN_COUNT_{userId}"], out loginCount);
            loginCount++;
            HttpContext.Response.Cookies.Append($"LOGIN_COUNT_{userId}", loginCount.ToString() ,new CookieOptions() { MaxAge = TimeSpan.MaxValue });
        }
    }
}