using CanvasArtStore.Data;
using CanvasArtStore.ViewModels.Category;
using CanvasArtStoreSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CanvasArtStoreSystem.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly CanvasArtStoreDbContext dbContext;

        public CategoryService(CanvasArtStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PaintingSelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<PaintingSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new PaintingSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .Categories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }
    }
}

