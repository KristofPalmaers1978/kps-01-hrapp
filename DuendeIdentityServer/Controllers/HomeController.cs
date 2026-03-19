using Duende.IdentityServer.Services;
using DuendeIdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;

namespace DuendeIdentityServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var duendeConfig = new DuendeConfigViewModel();
            return View(duendeConfig);
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    var x = "";
        //    var y = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        //    var z = "";
        //    return View();
        //}
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [Route("home/error")]
        public async Task<IActionResult> Error(string errorId)
        {
            var message = await _interaction.GetErrorContextAsync(errorId);
            return View("Error", message);
        }
        //[Route("home/error")]
        //public IActionResult Error(string errorId)
        //{
        //    var message = HttpContext.GetIdentityServerErrorMessage(errorId);
        //    return View("Error", message);
        //}
    }
}
