using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManagement.Core.PostAggregates;

namespace PostManagement.Infrastructure.EntityFrameworkCore.EntityTypeConfigurations;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts", "PM");

        builder.HasKey(t => t.Id);
        builder.Property(x => x.Id).IsRequired().UseIdentityColumn(1, 1);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(-1);
        builder.OwnsOne(x => x.Author, sub =>
        {
            sub.Property(x => x.Id).IsRequired().HasColumnName("AuthorId");
            sub.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnName("AuthorName");
        });
        builder.OwnsOne(x => x.Category, sub =>
        {
            sub.Property(x => x.Id).IsRequired().HasColumnName("CategoryId");
            sub.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnName("CategoryName");
        });
        builder.Property(x => x.Status).IsRequired().HasConversion(x => x.Value, x => PostStatus.FromValue(x));
        builder.Property(x => x.PublishedTime).IsRequired(false);

        builder.Ignore(x => x.DomainEvents);
    }
}
