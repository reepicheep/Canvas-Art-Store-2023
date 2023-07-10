using System.ComponentModel.DataAnnotations;

namespace CanvasArtStore.Web.ViewModels.Curator
{
    public class CuratorInfoOnPaintingViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
