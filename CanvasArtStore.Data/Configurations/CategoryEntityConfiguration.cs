using CanvasArtStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanvasArtStore.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Abstract"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Realistic"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Contemporary"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
