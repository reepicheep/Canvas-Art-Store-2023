using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CanvasArtStore.Common.EntityValidationConstants.Curator;

namespace CanvasArtStore.Data.Models
{
    public class Curator
    {
        public Curator()
        {
            this.Id = Guid.NewGuid();
            this.OwnedPaintings = new HashSet<Painting>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Painting> OwnedPaintings { get; set; }
    }
}
