using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManagement.Core.PostAggregates;
using Shared.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore.Configurations;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(-1);
        builder.Property(x => x.Status).IsRequired()
            .HasConversion(
                x => x.Value,
                x => PostStatus.FromValue(x));

        builder.OwnsOne(x => x.Author, sub => 
        {
            sub.Property(x => x.Id).IsRequired().HasColumnName("AuthorId");
            sub.Property(x => x.Name).IsRequired().HasMaxLength(20).HasColumnName("AuthorName");
        });

        builder.OwnsOne(x => x.Category, sub => 
        {
            sub.Property(x => x.Id).IsRequired().HasColumnName("CategoryId");
            sub.Property(x => x.Name).IsRequired().HasMaxLength(20).HasColumnName("CategoryName");
        });

        builder.ConcurrencyStamp(x => x.ConcurrencyStamp);
        builder.DeletionStatus(x => x.DeletionStatus);
    }
}
