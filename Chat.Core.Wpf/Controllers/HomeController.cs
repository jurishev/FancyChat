using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chat.Core.Wpf.Models;

namespace Chat.Core.Wpf.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
