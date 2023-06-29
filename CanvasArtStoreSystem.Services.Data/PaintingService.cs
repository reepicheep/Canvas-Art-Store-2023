using CanvasArtStore.Data;
using CanvasArtStore.ViewModels.Home;
using CanvasArtStoreSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CanvasArtStoreSystem.Services.Data
{
    public class PaintingService : IPaintingService
    {
        private readonly CanvasArtStoreDbContext dbContext;

        public PaintingService(CanvasArtStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreePaintingsAsync()
        {
            IEnumerable<IndexViewModel> lastThreePaintings = await this.dbContext
                .Paintings
                .OrderByDescending(p => p.CreatedOn)
                .Take(3)
                .Select(p => new IndexViewModel()
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    ImageUrl = p.ImageUrl
                })
                .ToArrayAsync();

            return lastThreePaintings;
        }
    }
}
