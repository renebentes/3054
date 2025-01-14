using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="Category"/> entity.
/// </summary>
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(Title.MaxLength);

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasColumnType("VARCHAR")
            .HasMaxLength(Description.MaxLength);
    }
}
