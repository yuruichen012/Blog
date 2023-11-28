﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.Infrastructure.EntityFrameworkCore.Configurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(x => x.Id).IsClustered(true);

        builder.Property(x => x.Id).IsRequired().UseIdentityColumn(1, 1);
        builder.Property(x => x.ParentId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
    }
}