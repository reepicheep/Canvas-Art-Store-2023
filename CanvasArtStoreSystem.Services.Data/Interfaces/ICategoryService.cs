using CanvasArtStore.Web.ViewModels.Category;

namespace CanvasArtStoreSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<PaintingSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
