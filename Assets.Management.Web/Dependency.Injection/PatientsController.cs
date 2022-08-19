using System.Text.Json;
using Assets.Management.Common.Interfaces;
using Assets.Management.Web.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Management.Web.Dependency.Injection;
    public class PatientsController : Controller
    {
        private readonly IPatientsServiceRepository _patientsServiceRepository;
        private readonly IConfiguration _configuration;
        public PatientsController(IPatientsServiceRepository patientsServiceRepository, IConfiguration configuration)
        {
           _patientsServiceRepository = patientsServiceRepository;
           _configuration = configuration;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        try
        {
            if (accessToken == null) return View("json");

            var apiGatewayConfig = new ApiGatewayConfig();

           _configuration.GetSection(apiGatewayConfig.ApiGateway).Bind(apiGatewayConfig);  

            //var content = await _patientsServiceRepository.GetPatients(accessToken, apiGatewayConfig.PatientsUriOcelot);

            var content = await _patientsServiceRepository.GetPatients(accessToken, apiGatewayConfig.PatientsUriYarp);

            var doc = JsonDocument.Parse(content).RootElement;
            ViewBag.Json = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });

            return View("json");
        }
        catch (Exception)
        {
            return View("AccessDenied");
        }
    }
    }
