using CanvasArtStore.Web.ViewModels.Painting;

namespace CanvasArtStoreSystem.Services.Data.Services.Data.Models.Painting
{
    public class AllPaintingsFilteredAndPagedServiceModel
    {
        public AllPaintingsFilteredAndPagedServiceModel()
        {
            this.Paintings = new HashSet<PaintingAllViewModel>();
        }

        public int TotalPaintingsCount { get; set; }

        public IEnumerable<PaintingAllViewModel> Paintings { get; set; }
    }
}
