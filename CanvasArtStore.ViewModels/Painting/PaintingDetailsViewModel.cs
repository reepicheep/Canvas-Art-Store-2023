using CanvasArtStore.Web.ViewModels.Curator;

namespace CanvasArtStore.Web.ViewModels.Painting
{
    public class PaintingDetailsViewModel : PaintingAllViewModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public CuratorInfoOnPaintingViewModel Curator { get; set; } = null!;
    }
}
