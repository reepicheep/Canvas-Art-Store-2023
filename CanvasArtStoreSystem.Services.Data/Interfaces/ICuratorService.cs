﻿using CanvasArtStore.Web.ViewModels.Curator;

namespace CanvasArtStoreSystem.Services.Data.Interfaces
{
    public interface ICuratorService
    {
        Task<bool> CuratorExistsByUserIdAsync(string userId);

        Task<bool> CuratorExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasBuysByUserIdAsync(string userId);

        Task Create(string userId, BecomeCuratorFormModel model);

        Task<string?> GetCuratorIdByUserIdAsync(string userId);

    }
}
 