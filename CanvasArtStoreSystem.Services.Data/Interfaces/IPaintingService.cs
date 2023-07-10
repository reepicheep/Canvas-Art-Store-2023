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

        Task<IEnumerable<PaintingAllViewModel>> AllByCuratorIdAsync(string curatorId);

        Task<IEnumerable<PaintingAllViewModel>> AllByUserIdAsync(string userId);

        Task<bool> ExistsByIdAsync(string houseId);

        Task<PaintingDetailsViewModel> GetDetailsByIdAsync(string paintingId);

        Task<PaintingFormModel> GetPaintingForEditByIdAsync(string houseId);

        Task<bool> IsCuratorWithIdOwnerOfPaintingWithIdAsync(string paintingId, string curatorId);

        Task EditPaintingByIdAndFormModelAsync(string paintingId, PaintingFormModel formModel);

        //Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

        //Task DeleteHouseByIdAsync(string houseId);
    }
}
