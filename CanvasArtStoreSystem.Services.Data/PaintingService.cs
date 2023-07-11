using CanvasArtStore.Data;
using CanvasArtStore.Data.Models;
using CanvasArtStore.Web.ViewModels.Curator;
using CanvasArtStore.Web.ViewModels.Home;
using CanvasArtStore.Web.ViewModels.Painting;
using CanvasArtStore.Web.ViewModels.Painting.Enums;
using CanvasArtStoreSystem.Services.Data.Interfaces;
using CanvasArtStoreSystem.Services.Data.Services.Data.Models.Painting;
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

        public async Task<string> CreateAndReturnIdAsync(PaintingFormModel formModel, string curatorId)
        {
            Painting newPainting = new() // Painting instead of ()
            {
                Title = formModel.Title,
                Author = formModel.Author,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Price = formModel.Price,
                CategoryId = formModel.CategoryId,
                CuratorId = Guid.Parse(curatorId),
            };

            await this.dbContext.Paintings.AddAsync(newPainting);
            await this.dbContext.SaveChangesAsync();

            return newPainting.Id.ToString();
        }

        public async Task<AllPaintingsFilteredAndPagedServiceModel> AllAsync(AllPaintingsQueryModel queryModel)
        {
            IQueryable<Painting> paintingsQuery = this.dbContext
                .Paintings
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                paintingsQuery = paintingsQuery
                    .Where(p => p.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                 
                paintingsQuery = paintingsQuery
                    .Where(p => EF.Functions.Like(p.Title, wildCard) ||
                                EF.Functions.Like(p.Author, wildCard) ||
                                EF.Functions.Like(p.Description, wildCard));
            }

            paintingsQuery = queryModel.PaintingSorting switch
            {
                PaintingSorting.Newest => paintingsQuery
                    .OrderByDescending(p => p.CreatedOn),
                PaintingSorting.Oldest => paintingsQuery
                    .OrderBy(p => p.CreatedOn),
                PaintingSorting.PriceAscending => paintingsQuery
                    .OrderBy(p => p.Price),
                PaintingSorting.PriceDescending => paintingsQuery
                    .OrderByDescending(p => p.Price),
                _ => paintingsQuery
                    .OrderBy(p => p.BuyerId != null)
                    .ThenByDescending(p => p.CreatedOn)
            };

            IEnumerable<PaintingAllViewModel> allPaintings = await paintingsQuery
                .Where(p => p.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.PaintingsPerPage)
                .Take(queryModel.PaintingsPerPage)
                .Select(p => new PaintingAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Author = p.Author,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsBought = p.BuyerId.HasValue
                })
                .ToArrayAsync();

            int totalPaintings = paintingsQuery.Count();

            return new AllPaintingsFilteredAndPagedServiceModel()
            {
                TotalPaintingsCount = totalPaintings,
                Paintings = allPaintings
            };
        }

        public async Task<IEnumerable<PaintingAllViewModel>> AllByCuratorIdAsync(string curatorId)
        {
            IEnumerable<PaintingAllViewModel> allCuratorHouses = await this.dbContext
                .Paintings
                .Where(p => p.IsActive &&
                            p.CuratorId.ToString() == curatorId)
                .Select(p => new PaintingAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Author = p.Author,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsBought = p.BuyerId.HasValue
                })
                .ToArrayAsync();

            return allCuratorHouses;
        }

        public async Task<IEnumerable<PaintingAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<PaintingAllViewModel> allUserPaintings = await this.dbContext
                .Paintings
                .Where(p => p.IsActive &&
                            p.BuyerId.HasValue &&
                            p.BuyerId.ToString() == userId)
                .Select(p => new PaintingAllViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Author = p.Author,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsBought = p.BuyerId.HasValue
                })
                .ToArrayAsync();

            return allUserPaintings;
        }

        public async Task<bool> ExistsByIdAsync(string paintingId)
        {
            bool result = await this.dbContext
                .Paintings
                .Where(p => p.IsActive)
                .AnyAsync(p => p.Id.ToString() == paintingId);

            return result;
        }

        public async Task<PaintingDetailsViewModel> GetDetailsByIdAsync(string paintingId)
        {
            Painting painting = await this.dbContext
                .Paintings
                .Include(p => p.Category)
                .Include(p => p.Curator)
                .ThenInclude(a => a.User)
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            return new PaintingDetailsViewModel
            {
                Id = painting.Id.ToString(),
                Title = painting.Title,
                Author = painting.Author,
                ImageUrl = painting.ImageUrl,
                Price = painting.Price,
                IsBought = painting.BuyerId.HasValue,
                Description = painting.Description,
                Category = painting.Category.Name,
                Curator = new CuratorInfoOnPaintingViewModel()
                {
                    Email = painting.Curator.User.Email,
                    PhoneNumber = painting.Curator.PhoneNumber
                }
            };
        }

        public async Task<PaintingFormModel> GetPaintingForEditByIdAsync(string paintingId)
        {
            Painting painting = await this.dbContext
                .Paintings
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            return new PaintingFormModel
            {
                Title = painting.Title,
                Author = painting.Author,
                Description = painting.Description,
                ImageUrl = painting.ImageUrl,
                Price = painting.Price,
                CategoryId = painting.CategoryId,
            };
        }

        public async Task<bool> IsCuratorWithIdOwnerOfPaintingWithIdAsync(string paintingId, string curatorId)
        {
            Painting painting = await this.dbContext
                .Paintings
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            return painting.CuratorId.ToString() == curatorId;
        }

        public async Task EditPaintingByIdAndFormModelAsync(string paintingId, PaintingFormModel formModel)
        {
            Painting painting = await this.dbContext
                .Paintings
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            painting.Title = formModel.Title;
            painting.Author = formModel.Author;
            painting.Description = formModel.Description;
            painting.ImageUrl = formModel.ImageUrl;
            painting.Price = formModel.Price;
            painting.CategoryId = formModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<PaintingPreDeleteDetailsViewModel> GetPaintingForDeleteByIdAsync(string paintingId)
        {
            Painting painting = await this.dbContext
                .Paintings
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            return new PaintingPreDeleteDetailsViewModel
            {
                Title = painting.Title,
                Author = painting.Author,
                ImageUrl = painting.ImageUrl
            };
        }

        public async Task DeletePaintingByIdAsync(string paintingId)
        {
            Painting paintingToDelete = await this.dbContext
                .Paintings
                .Where(p => p.IsActive)
                .FirstAsync(p => p.Id.ToString() == paintingId);

            paintingToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }
    }
}