using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "PM");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired().UseIdentityColumn(1, 1);
        builder.Property(p => p.ParentId).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.Ignore(p => p.DomainEvents);
    }
}
