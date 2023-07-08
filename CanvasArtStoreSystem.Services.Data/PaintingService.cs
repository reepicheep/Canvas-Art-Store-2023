using CanvasArtStore.Data;
using CanvasArtStore.Data.Models;
using CanvasArtStore.ViewModels.Home;
using CanvasArtStore.ViewModels.Painting;
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

        public async Task<string> CreateAndReturnIdAsync(PaintingFormModel formModel, string curatorId)
        {
            Painting newPainting = new Painting
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

        //public async Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel)
        //{
        //    IQueryable<House> housesQuery = this.dbContext
        //        .Houses
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(queryModel.Category))
        //    {
        //        housesQuery = housesQuery
        //            .Where(h => h.Category.Name == queryModel.Category);
        //    }

        //    if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
        //    {
        //        string wildCard = $"%{queryModel.SearchString.ToLower()}%";

        //        housesQuery = housesQuery
        //            .Where(h => EF.Functions.Like(h.Title, wildCard) ||
        //                        EF.Functions.Like(h.Address, wildCard) ||
        //                        EF.Functions.Like(h.Description, wildCard));
        //    }

        //    housesQuery = queryModel.HouseSorting switch
        //    {
        //        HouseSorting.Newest => housesQuery
        //            .OrderByDescending(h => h.CreatedOn),
        //        HouseSorting.Oldest => housesQuery
        //            .OrderBy(h => h.CreatedOn),
        //        HouseSorting.PriceAscending => housesQuery
        //            .OrderBy(h => h.PricePerMonth),
        //        HouseSorting.PriceDescending => housesQuery
        //            .OrderByDescending(h => h.PricePerMonth),
        //        _ => housesQuery
        //            .OrderBy(h => h.RenterId != null)
        //            .ThenByDescending(h => h.CreatedOn)
        //    };

        //    IEnumerable<HouseAllViewModel> allHouses = await housesQuery
        //        .Where(h => h.IsActive)
        //        .Skip((queryModel.CurrentPage - 1) * queryModel.HousesPerPage)
        //        .Take(queryModel.HousesPerPage)
        //        .Select(h => new HouseAllViewModel
        //        {
        //            Id = h.Id.ToString(),
        //            Title = h.Title,
        //            Address = h.Address,
        //            ImageUrl = h.ImageUrl,
        //            PricePerMonth = h.PricePerMonth,
        //            IsRented = h.RenterId.HasValue
        //        })
        //        .ToArrayAsync();
        //    int totalHouses = housesQuery.Count();

        //    return new AllHousesFilteredAndPagedServiceModel()
        //    {
        //        TotalHousesCount = totalHouses,
        //        Houses = allHouses
        //    };
        //}

        //public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        //{
        //    IEnumerable<HouseAllViewModel> allAgentHouses = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive &&
        //                    h.AgentId.ToString() == agentId)
        //        .Select(h => new HouseAllViewModel
        //        {
        //            Id = h.Id.ToString(),
        //            Title = h.Title,
        //            Address = h.Address,
        //            ImageUrl = h.ImageUrl,
        //            PricePerMonth = h.PricePerMonth,
        //            IsRented = h.RenterId.HasValue
        //        })
        //        .ToArrayAsync();

        //    return allAgentHouses;
        //}

        //public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId)
        //{
        //    IEnumerable<HouseAllViewModel> allUserHouses = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive &&
        //                    h.RenterId.HasValue &&
        //                    h.RenterId.ToString() == userId)
        //        .Select(h => new HouseAllViewModel
        //        {
        //            Id = h.Id.ToString(),
        //            Title = h.Title,
        //            Address = h.Address,
        //            ImageUrl = h.ImageUrl,
        //            PricePerMonth = h.PricePerMonth,
        //            IsRented = h.RenterId.HasValue
        //        })
        //        .ToArrayAsync();

        //    return allUserHouses;
        //}

        //public async Task<bool> ExistsByIdAsync(string houseId)
        //{
        //    bool result = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive)
        //        .AnyAsync(h => h.Id.ToString() == houseId);

        //    return result;
        //}

        //public async Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId)
        //{
        //    House house = await this.dbContext
        //        .Houses
        //        .Include(h => h.Category)
        //        .Include(h => h.Agent)
        //        .ThenInclude(a => a.User)
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    return new HouseDetailsViewModel
        //    {
        //        Id = house.Id.ToString(),
        //        Title = house.Title,
        //        Address = house.Address,
        //        ImageUrl = house.ImageUrl,
        //        PricePerMonth = house.PricePerMonth,
        //        IsRented = house.RenterId.HasValue,
        //        Description = house.Description,
        //        Category = house.Category.Name,
        //        Agent = new AgentInfoOnHouseViewModel()
        //        {
        //            Email = house.Agent.User.Email,
        //            PhoneNumber = house.Agent.PhoneNumber
        //        }
        //    };
        //}

        //public async Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId)
        //{
        //    House house = await this.dbContext
        //        .Houses
        //        .Include(h => h.Category)
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    return new HouseFormModel
        //    {
        //        Title = house.Title,
        //        Address = house.Address,
        //        Description = house.Description,
        //        ImageUrl = house.ImageUrl,
        //        PricePerMonth = house.PricePerMonth,
        //        CategoryId = house.CategoryId,
        //    };
        //}

        //public async Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId)
        //{
        //    House house = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    return house.AgentId.ToString() == agentId;
        //}

        //public async Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel formModel)
        //{
        //    House house = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    house.Title = formModel.Title;
        //    house.Address = formModel.Address;
        //    house.Description = formModel.Description;
        //    house.ImageUrl = formModel.ImageUrl;
        //    house.PricePerMonth = formModel.PricePerMonth;
        //    house.CategoryId = formModel.CategoryId;

        //    await this.dbContext.SaveChangesAsync();
        //}

        //public async Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId)
        //{
        //    House house = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    return new HousePreDeleteDetailsViewModel
        //    {
        //        Title = house.Title,
        //        Address = house.Address,
        //        ImageUrl = house.ImageUrl
        //    };
        //}

        //public async Task DeleteHouseByIdAsync(string houseId)
        //{
        //    House houseToDelete = await this.dbContext
        //        .Houses
        //        .Where(h => h.IsActive)
        //        .FirstAsync(h => h.Id.ToString() == houseId);

        //    houseToDelete.IsActive = false;

        //    await this.dbContext.SaveChangesAsync();
        //}
    }
}
