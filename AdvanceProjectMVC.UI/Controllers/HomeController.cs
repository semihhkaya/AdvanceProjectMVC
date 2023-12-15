using AdvanceProjectMVC.Dto.Employee;
using AdvanceProjectMVC.UI.Extensions;
using AdvanceProjectMVC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");

            // ViewBag ile taşıdım
            ViewBag.EmployeeFullName = $"{currentUser?.Name} {currentUser?.Surname}";
            ViewBag.EmployeeTitle = currentUser?.Title?.TitleName;

            
            var userClaims = User.Claims.ToList();

            
            ViewBag.UserName = User.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
