using CanvasArtStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanvasArtStore.Data.Configurations
{
    public class PaintingEntityConfiguration : IEntityTypeConfiguration<Painting>
    {
        public void Configure(EntityTypeBuilder<Painting> builder)
        {
            builder
                .Property(p => p.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Paintings)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Curator)
                .WithMany(a => a.OwnedPaintings)
                .HasForeignKey(p => p.CuratorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GeneratePaintings());
        }

        private Painting[] GeneratePaintings()
        {
            ICollection<Painting> paintings = new HashSet<Painting>();

            Painting painting;

            painting = new Painting()
            {
                Title = "The Order of Chaos",
                Author = "Joel Filipe, Unsplash",
                Description = "This painting was made with some experimental liquids as milk, water paint and oil.",
                ImageUrl = "https://bityl.co/JYUO",
                Price = 1200.00M,
                CategoryId = 1,
                CuratorId = Guid.Parse("83DA74C8-B268-4D7E-A527-65082DFCE13D"), //AgentId
                BuyerId = Guid.Parse("DA6FA3E5-9921-4AAE-8E9B-502AE65A27F1") //UserId
            };
            paintings.Add(painting);

            painting = new Painting()
            {
                Title = "The Tractor, 1933",
                Author = "Eric Ravilious (d. 1942)",
                Description = "Thanks to Birmingham Museums Trust, the UK.",
                ImageUrl = "https://bityl.co/JYU5",
                Price = 1200.00M,
                CategoryId = 2,
                CuratorId = Guid.Parse("83DA74C8-B268-4D7E-A527-65082DFCE13D"), //AgentId
            };
            paintings.Add(painting);

            painting = new Painting()
            {
                Title = "No Name Contemporary",
                Author = "Kseniya Lapteva",
                Description = "Painting and HD Art Wallpaper",
                ImageUrl = "https://bityl.co/JYUS",
                Price = 2000.00M,
                CategoryId = 3,
                CuratorId = Guid.Parse("83DA74C8-B268-4D7E-A527-65082DFCE13D"), //AgentId
            };
            paintings.Add(painting);

            return paintings.ToArray();
        }
    }
}
