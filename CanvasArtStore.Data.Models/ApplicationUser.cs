using Microsoft.AspNetCore.Identity;

namespace CanvasArtStore.Data.Models
{
    /// <summary>
    /// This is custom user class that works with the default ASP.NET Core Identity.
    /// You can add additional info to the built-in users.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.BoughtPaintings = new HashSet<Painting>();
        }

        public virtual ICollection<Painting> BoughtPaintings { get; set; }
    }
}
