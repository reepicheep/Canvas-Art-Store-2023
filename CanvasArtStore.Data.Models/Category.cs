using static CanvasArtStore.Common.EntityValidationConstants.Category;
using System.ComponentModel.DataAnnotations;

//using static Common.EntityValidationConstants.Category;

namespace CanvasArtStore.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Paintings = new HashSet<Painting>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}