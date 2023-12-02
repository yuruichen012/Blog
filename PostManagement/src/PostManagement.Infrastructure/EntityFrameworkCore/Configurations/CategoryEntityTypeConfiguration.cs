using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManagement.Core.CategoryAggregates;
using Shared.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore.Configurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.ParentId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

        builder.ConcurrencyStamp(x => x.ConcurrencyStamp);
        builder.DeletionStatus(x => x.DeletionStatus);
    }
}
