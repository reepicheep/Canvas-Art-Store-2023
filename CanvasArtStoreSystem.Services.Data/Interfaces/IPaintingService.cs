using CanvasArtStore.Web.ViewModels.Home;
using CanvasArtStore.Web.ViewModels.Painting;
using CanvasArtStoreSystem.Services.Data.Services.Data.Models.Painting;

namespace CanvasArtStoreSystem.Services.Data.Interfaces
{
    public interface IPaintingService
    {
        Task<IEnumerable<IndexViewModel>> LastThreePaintingsAsync();

        Task<string> CreateAndReturnIdAsync(PaintingFormModel formModel, string curatorId);

        Task<AllPaintingsFilteredAndPagedServiceModel> AllAsync(AllPaintingsQueryModel queryModel);

        //Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);

        //Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);

        //Task<bool> ExistsByIdAsync(string houseId);

        //Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

        //Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId);

        //Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId);

        //Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel formModel);

        //Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

        //Task DeleteHouseByIdAsync(string houseId);
    }
}
