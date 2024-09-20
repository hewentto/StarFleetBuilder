using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarFleetBuilder.Data;
using StarFleetBuilder.Models;
using StarFleetBuilder.Services;
using StarFleetBuilder.ViewModels;
using System.Diagnostics;

namespace StarFleetBuilder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StarFleetBuilderContext _context;
        private readonly StarshipService _starshipService;

        public HomeController(ILogger<HomeController> logger, StarFleetBuilderContext context, StarshipService starshipService)
        {
            _logger = logger;
            _context = context;
            _starshipService = starshipService;
        }

        public async Task<IActionResult> Index()
        {
            var randomStarship = await _starshipService.GetRandomStarshipAsync();

            var viewModel = new HomeViewModel
            {
                RandomShipName = randomStarship.Name,
                RandomShipUrl = randomStarship.Url
            };

            return View(viewModel);
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
