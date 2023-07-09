using CanvasArtStore.Web.ViewModels.Home;
using CanvasArtStoreSystem.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CanvasArtStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaintingService paintingService;

        public HomeController(IPaintingService paintingService)
        {
            this.paintingService = paintingService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel =
               await paintingService.LastThreePaintingsAsync();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}