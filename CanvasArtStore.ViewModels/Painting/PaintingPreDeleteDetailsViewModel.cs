using System.ComponentModel.DataAnnotations;

namespace CanvasArtStore.Web.ViewModels.Painting
{
    public class PaintingPreDeleteDetailsViewModel
    {
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
