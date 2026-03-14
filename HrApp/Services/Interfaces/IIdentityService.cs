using HrApp.Services.Results;
using HrApp.ViewModels;

namespace HrApp.Services.Interfaces
{
    namespace HrApp.Services
    {
        public interface IIdentityService
        {
            Task<IdentityServiceResult> LoginAsync(string username, string email, string password);
            Task SignOutAsync();
            Task<IdentityServiceResult> RegisterAsync(RegisterViewModel registerData);
        }
    }
}
