using HrApp.Services.Interfaces.HrApp.Services;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Policy;

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
        SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IIdentityService identityService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _identityService = identityService;
            _signInManager = signInManager;
            _userManager = userManager;
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

        public IActionResult LoginExternalProvider()
        {
            string? redirectUrl = Url.Action("ExternalProviderResponse", "Account");
            string scheme = "oidc";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                    scheme, redirectUrl);
            return new ChallengeResult(scheme, properties);
        }
        public async Task<IActionResult> ExternalProviderResponse()
        {
            ExternalLoginInfo? externalLoginInfo =
                await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var user = await _userManager.FindByLoginAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey);
                if (user == null)
                {
                    user = await CreateIdentityUserFromClaims(externalLoginInfo);
                }
                await _signInManager.SignInAsync(user, true);
            }
            return RedirectToAction("Index", "Home");
        }
        private async Task<IdentityUser?> CreateIdentityUserFromClaims(ExternalLoginInfo externalLoginInfo)
        {
            var claim = externalLoginInfo.Principal.FindFirst("email");
            if(claim != null)
            {
                var email = claim.Value;
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new IdentityUser {UserName = email, Email = email };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        return null;
                    }
                }
                var loginResult = await _userManager.AddLoginAsync(user, externalLoginInfo);
                if (loginResult.Succeeded)
                {
                    return user; 
                }
            }
            return null;
        }

    }
}

