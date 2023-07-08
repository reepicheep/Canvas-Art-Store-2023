using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CanvasArtStore.Web.Controllers
{
    [Authorize]
    public class PaintingController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isCurator =
                await this.curatorService.CuratorExistsByUserIdAsync(this.User.GetId()!);
            if (!isCurator)
            {
                this.TempData[ErrorMessage] = "You must become a curator in order to add new paintings!";

                return this.RedirectToAction("Become", "Curator");
            }

            try
            {
                PaintingFormModel formModel = new PaintingFormModel()
                {
                    Categories = await this.categoryService.AllCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaintingFormModel model)
        {
            bool isCurator =
                await this.curatorService.CuratorExistsByUserIdAsync(this.User.GetId()!);
            if (!isCurator)
            {
                this.TempData[ErrorMessage] = "You must become a curator in order to add new paintings!";

                return this.RedirectToAction("Become", "Curator");
            }

            bool categoryExists =
                await this.categoryService.ExistsByIdAsync(model.CategoryId);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? curatorId =
                    await this.curatorService.GetCuratorIdByUserIdAsync(this.User.GetId()!);

                string paintingId =
                    await this.paintingService.CreateAndReturnIdAsync(model, curatorId!);

                this.TempData[SuccessMessage] = "Painting was added successfully!";
                return this.RedirectToAction("Details", "Painting", new { id = paintingId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new painting! Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(model);
            }
        }
    }
}
