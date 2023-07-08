using CanvasArtStore.ViewModels.Home;

namespace CanvasArtStoreSystem.Services.Data.Interfaces
{
    public interface IPaintingService
    {
        Task<IEnumerable<IndexViewModel>> LastThreePaintingsAsync();

        Task<string> CreateAndReturnIdAsync(CanvasArtStore.ViewModels.Painting.PaintingFormModel formModel, string curatorId);

        //Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);

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
