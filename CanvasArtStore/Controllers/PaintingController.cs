using CanvasArtStore.Web.Infrastructure.Extensions;
using CanvasArtStore.Web.ViewModels.Painting;
using CanvasArtStoreSystem.Services.Data.Interfaces;
using CanvasArtStoreSystem.Services.Data.Services.Data.Models.Painting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static CanvasArtStore.Common.NotificationMessagesConstants;

namespace CanvasArtStore.Web.Controllers;

[Authorize]
public class PaintingController : Controller
{
    private readonly ICategoryService categoryService;
    private readonly ICuratorService curatorService;
    private readonly IPaintingService paintingService;

    public PaintingController(ICategoryService categoryService, ICuratorService curatorService, IPaintingService paintingService)
    {
        this.categoryService = categoryService;
        this.curatorService = curatorService;
        this.paintingService = paintingService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllPaintingsQueryModel queryModel)
    {
        AllPaintingsFilteredAndPagedServiceModel serviceModel =
            await this.paintingService.AllAsync(queryModel);

        queryModel.Paintings = serviceModel.Paintings;
        queryModel.TotalPaintings = serviceModel.TotalPaintingsCount;
        queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

        return this.View(queryModel);
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
            PaintingFormModel formModel = new() // PaintingFormModel()
            {
                Categories = await this.categoryService.AllCategoriesAsync()
            };

            return View(formModel);
        }
        catch (Exception)
        {
            //return View(ErrorMessage); // check this and replace with below!
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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        bool paintingExists = await this.paintingService
            .ExistsByIdAsync(id);
        if (!paintingExists)
        {
            this.TempData[ErrorMessage] = "Painting with the provided id does not exist!";

            return this.RedirectToAction("All", "Painting");
        }

        try
        {
            PaintingDetailsViewModel viewModel = await this.paintingService
                .GetDetailsByIdAsync(id);

            return View(viewModel);
        }
        catch (Exception)
        {
            return this.GeneralError();
        }
    }

    //[HttpGet]
    //public async Task<IActionResult> Edit(string id)
    //{
    //    bool houseExists = await this.houseService
    //        .ExistsByIdAsync(id);
    //    if (!houseExists)
    //    {
    //        this.TempData[ErrorMessage] = "House with the provided id does not exist!";

    //        return this.RedirectToAction("All", "House");
    //    }

    //    bool isUserAgent = await this.agentService
    //        .AgentExistsByUserIdAsync(this.User.GetId()!);
    //    if (!isUserAgent)
    //    {
    //        this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

    //        return this.RedirectToAction("Become", "Agent");
    //    }

    //    string? agentId =
    //        await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
    //    bool isAgentOwner = await this.houseService
    //        .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
    //    if (!isAgentOwner)
    //    {
    //        this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

    //        return this.RedirectToAction("Mine", "House");
    //    }

    //    try
    //    {
    //        HouseFormModel formModel = await this.houseService
    //            .GetHouseForEditByIdAsync(id);
    //        formModel.Categories = await this.categoryService.AllCategoriesAsync();

    //        return this.View(formModel);
    //    }
    //    catch (Exception)
    //    {
    //        return this.GeneralError();
    //    }
    //}

    //[HttpPost]
    //public async Task<IActionResult> Edit(string id, HouseFormModel model)
    //{
    //    if (!this.ModelState.IsValid)
    //    {
    //        model.Categories = await this.categoryService.AllCategoriesAsync();

    //        return this.View(model);
    //    }

    //    bool houseExists = await this.houseService
    //        .ExistsByIdAsync(id);
    //    if (!houseExists)
    //    {
    //        this.TempData[ErrorMessage] = "House with the provided id does not exist!";

    //        return this.RedirectToAction("All", "House");
    //    }

    //    bool isUserAgent = await this.agentService
    //        .AgentExistsByUserIdAsync(this.User.GetId()!);
    //    if (!isUserAgent)
    //    {
    //        this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

    //        return this.RedirectToAction("Become", "Agent");
    //    }

    //    string? agentId =
    //        await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
    //    bool isAgentOwner = await this.houseService
    //        .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
    //    if (!isAgentOwner)
    //    {
    //        this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

    //        return this.RedirectToAction("Mine", "House");
    //    }

    //    try
    //    {
    //        await this.houseService.EditHouseByIdAndFormModelAsync(id, model);
    //    }
    //    catch (Exception)
    //    {
    //        this.ModelState.AddModelError(string.Empty,
    //            "Unexpected error occurred while trying to update the house. Please try again later or contact administrator!");
    //        model.Categories = await this.categoryService.AllCategoriesAsync();

    //        return this.View(model);
    //    }

    //    this.TempData[SuccessMessage] = "House was edited successfully!";
    //    return this.RedirectToAction("Details", "House", new { id });
    //}

    //[HttpGet]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    bool houseExists = await this.houseService
    //        .ExistsByIdAsync(id);
    //    if (!houseExists)
    //    {
    //        this.TempData[ErrorMessage] = "House with the provided id does not exist!";

    //        return this.RedirectToAction("All", "House");
    //    }

    //    bool isUserAgent = await this.agentService
    //        .AgentExistsByUserIdAsync(this.User.GetId()!);
    //    if (!isUserAgent)
    //    {
    //        this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

    //        return this.RedirectToAction("Become", "Agent");
    //    }

    //    string? agentId =
    //        await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
    //    bool isAgentOwner = await this.houseService
    //        .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
    //    if (!isAgentOwner)
    //    {
    //        this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

    //        return this.RedirectToAction("Mine", "House");
    //    }

    //    try
    //    {
    //        HousePreDeleteDetailsViewModel viewModel =
    //            await this.houseService.GetHouseForDeleteByIdAsync(id);

    //        return this.View(viewModel);
    //    }
    //    catch (Exception)
    //    {
    //        return this.GeneralError();
    //    }
    //}

    //[HttpPost]
    //public async Task<IActionResult> Delete(string id, HousePreDeleteDetailsViewModel model)
    //{
    //    bool houseExists = await this.houseService
    //        .ExistsByIdAsync(id);
    //    if (!houseExists)
    //    {
    //        this.TempData[ErrorMessage] = "House with the provided id does not exist!";

    //        return this.RedirectToAction("All", "House");
    //    }

    //    bool isUserAgent = await this.agentService
    //        .AgentExistsByUserIdAsync(this.User.GetId()!);
    //    if (!isUserAgent)
    //    {
    //        this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

    //        return this.RedirectToAction("Become", "Agent");
    //    }

    //    string? agentId =
    //        await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
    //    bool isAgentOwner = await this.houseService
    //        .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
    //    if (!isAgentOwner)
    //    {
    //        this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

    //        return this.RedirectToAction("Mine", "House");
    //    }

    //    try
    //    {
    //        await this.houseService.DeleteHouseByIdAsync(id);

    //        this.TempData[WarningMessage] = "The house was successfully deleted!";
    //        return this.RedirectToAction("Mine", "House");
    //    }
    //    catch (Exception)
    //    {
    //        return this.GeneralError();
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        List<PaintingAllViewModel> myPaintings =
            new(); // new List<PaintingAllViewModel>();

        string userId = this.User.GetId()!;
        bool isUserCurator = await this.curatorService
            .CuratorExistsByUserIdAsync(userId);

        try
        {
            if (isUserCurator)
            {
                string? curatorId =
                    await this.curatorService.GetCuratorIdByUserIdAsync(userId);

                myPaintings.AddRange(await this.paintingService.AllByCuratorIdAsync(curatorId!));
            }
            else
            {
                myPaintings.AddRange(await this.paintingService.AllByUserIdAsync(userId));
            }

            return this.View(myPaintings);
        }
        catch (Exception)
        {
            return this.GeneralError();
        }
    }

    private IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] =
            "Unexpected error occurred! Please try again later or contact administrator";

        return this.RedirectToAction("Index", "Home");
    }
}
