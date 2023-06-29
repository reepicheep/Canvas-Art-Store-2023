using CanvasArtStore.ViewModels.Home;

namespace CanvasArtStoreSystem.Services.Data.Interfaces
{
    public interface IPaintingService
    {
        Task<IEnumerable<IndexViewModel>> LastThreePaintingsAsync();
    }
}
