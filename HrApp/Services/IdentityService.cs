using HrApp.Services.Interfaces.HrApp.Services;
using HrApp.Services.Results;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HrApp.Services
{
    public class IdentityService : IIdentityService
    {
        SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<IdentityServiceResult> LoginAsync(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityServiceResult> RegisterAsync(RegisterViewModel registerModel)
        {
            var result = new IdentityServiceResult();
            try
            {
                var identityUser = new IdentityUser
                {
                    Email = registerModel.Email,
                    UserName = registerModel.UserName
                };
                result.IdentityResult = await _userManager.CreateAsync(identityUser, registerModel.Password);
                if (!result.IdentityResult.Succeeded)
                {                    
                    foreach (var error in result.IdentityResult.Errors)
                    {
                        result.Failed(error.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                result.Failed(ex.Message);
            }
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
