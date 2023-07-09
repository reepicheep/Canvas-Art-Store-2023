using CanvasArtStore.Web.ViewModels.Painting.Enums;
using System.ComponentModel.DataAnnotations;
using static CanvasArtStore.Common.GeneralApplicationConstants;

namespace CanvasArtStore.Web.ViewModels.Painting
{
    public class AllPaintingsQueryModel
    {
        public AllPaintingsQueryModel()
        {
            CurrentPage = DefaultPage;
            PaintingsPerPage = EntitiesPerPage;

            Categories = new HashSet<string>();
            Paintings = new HashSet<PaintingAllViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Paintings By")]
        public PaintingSorting PaintingSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Paintings On Page")]
        public int PaintingsPerPage { get; set; }

        public int TotalPaintings { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<PaintingAllViewModel> Paintings { get; set; }
    }
}
