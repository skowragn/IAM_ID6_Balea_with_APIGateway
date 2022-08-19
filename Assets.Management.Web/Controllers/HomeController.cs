using System.Diagnostics;
using Assets.Management.Common;
using Assets.Management.Common.Attributes;
using Assets.Management.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Management.Web.Controllers;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {

            var accessToken = HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();

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

        [AuthorizeRoles(nameof(Roles.Doctor), nameof(Roles.Nurse))]
        public async Task<IActionResult> PrescribeMedication(string name, int amount)
        {
           return View("Success");
        }

        [AuthorizeRoles(nameof(Roles.Doctor))]
        public async Task<IActionResult> PerformSurgery()
        {
            return View("Success");
        }


        [Authorize(AllPermissions.PerformSurgery)]
        public async Task<IActionResult> PerformSurgeryText()
        {
            return View("AccessSurgeries");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
    }