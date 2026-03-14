using HrApp.Services.Interfaces.HrApp.Services;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;

        //public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login Username

        [HttpGet]
        public IActionResult LoginUserName()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginUserName(LoginUserNameViewModel model)
        {

            return View();
        }
        //TODO

        #endregion

        #region Login Email

        [HttpGet]
        public IActionResult LoginEmail()
        {
            return View();
        }

        //TODO
        [HttpPost]
        public async Task<IActionResult> LoginEmailAsync(LoginEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var identityUser = await _userManager.FindByEmailAsync(model.Email);
                //if (identityUser != null)
                //{
                //    var result = await _signInManager.PasswordSignInAsync(
                //        identityUser, model.Password, false, false);
                //    if (result.Succeeded)
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
                //ModelState.AddModelError("", "Problems signing in!");
            }
            return View();
        }
        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.RegisterAsync(registerModel);
                if (!result.Succeeded)
                {

                    ModelState.AddModelError("", result.ErrorString);
                }
                else
                {
                    //var login=new loginv
                    return View("Login");
                }
            }
            return View();
        }

        #endregion

        #region Logout

        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
