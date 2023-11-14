using GameStore.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Configurations
{
    internal class GameCategoryConfiguration : IEntityTypeConfiguration<GameCategory>
    {
        public void Configure(EntityTypeBuilder<GameCategory> builder)
        {
            builder.HasKey(i => new { i.GameId, i.CategoryId });

            builder.HasOne(c => c.Game)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(c => c.GameId);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
