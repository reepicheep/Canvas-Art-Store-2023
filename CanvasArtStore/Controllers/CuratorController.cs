using CanvasArtStoreSystem.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CanvasArtStore.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;
using static CanvasArtStore.Common.NotificationMessagesConstants;
using CanvasArtStore.Web.ViewModels.Curator;

namespace CanvasArtStore.Web.Controllers
{
    [Authorize]
    public class CuratorController : Controller
    {
        private readonly ICuratorService curatorService;

        public CuratorController(ICuratorService curatorService)
        {
            this.curatorService = curatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = User.GetId();
            bool isCurator = await curatorService.CuratorExistsByUserIdAsync(userId);
            if (isCurator)
            {
                TempData[ErrorMessage] = "You are already a curator!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeCuratorFormModel model)
        {
            string? userId = User.GetId();
            bool isCurator = await curatorService.CuratorExistsByUserIdAsync(userId);
            if (isCurator)
            {
                TempData[ErrorMessage] = "You are already a curator!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await curatorService.CuratorExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Curator with the provided phone number already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool userHasActiveBuys = await curatorService
                .HasBuysByUserIdAsync(userId);
            if (userHasActiveBuys)
            {
                TempData[ErrorMessage] = "You must not have any active buys in order to become a curator!";

                return RedirectToAction("Mine", "Painting");
            }

            try
            {
                await curatorService.Create(userId, model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as a curator! Please try again later or contact administrator.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("All", "Painting");
        }
    }
}
