using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Categories;

/// <summary>
/// Represents the configuration for the <see cref="Category"/> entity.
/// </summary>
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));

        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Title)
            .Property(t => t.Value)
            .HasColumnName(nameof(Category.Title))
            .HasColumnType("text")
            .HasMaxLength(Title.MaxLength)
            .IsRequired();

        builder.OwnsOne(c => c.Description)
            .Property(d => d.Text)
            .HasColumnName(nameof(Category.Description))
            .HasColumnType("text")
            .HasMaxLength(Description.MaxLength)
            .IsRequired(false);
    }
}
