using CanvasArtStore.Data;
using CanvasArtStore.Data.Models;
using CanvasArtStoreSystem.Services.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using CanvasArtStore.ViewModels.Curator;
 
namespace CanvasArtStoreSystem.Services.Data
{
    public class CuratorService : ICuratorService
    {
        private readonly CanvasArtStoreDbContext dbContext;

        public CuratorService(CanvasArtStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CuratorExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Curators
                .AnyAsync(c => c.UserId.ToString() == userId);

            return result;
        }

        public async Task<bool> CuratorExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Curators
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> HasBuysByUserIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return false;
            }

            return user.BoughtPaintings.Any();
        }

        public async Task Create(string userId, BecomeCuratorFormModel model)
        {
            Curator newCurator = new()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Curators.AddAsync(newCurator);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
