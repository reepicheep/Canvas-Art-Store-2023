using CanvasArtStoreSystem.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CanvasArtStore.Web.Infrastructure.Extensions.ClaimsPrincipalExtensions;
using static CanvasArtStore.Common.NotificationMessagesConstants;
using CanvasArtStore.ViewModels.Curator;

namespace CanvasArtStore.Controllers
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
            string? userId = this.User.GetId();
            bool isCurator = await this.curatorService.CuratorExistsByUserIdAsync(userId);
            if (isCurator)
            {
                this.TempData[ErrorMessage] = "You are already a curator!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeCuratorFormModel model)
        {
            string? userId = this.User.GetId();
            bool isCurator = await this.curatorService.CuratorExistsByUserIdAsync(userId);
            if (isCurator)
            {
                this.TempData[ErrorMessage] = "You are already a curator!";

                return this.RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await this.curatorService.CuratorExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Curator with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool userHasActiveBuys = await this.curatorService
                .HasBuysByUserIdAsync(userId);
            if (userHasActiveBuys)
            {
                this.TempData[ErrorMessage] = "You must not have any active buys in order to become a curator!";

                return this.RedirectToAction("Mine", "Painting");
            }

            try
            {
                await this.curatorService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as a curator! Please try again later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Painting");
        }
    }
}
