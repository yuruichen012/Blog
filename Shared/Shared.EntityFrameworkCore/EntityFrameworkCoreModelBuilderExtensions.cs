using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Ddd;
using System.Linq.Expressions;

namespace Shared.EntityFrameworkCore
{
    public static class EntityFrameworkCoreModelBuilderExtensions
    {
        public static void ConcurrencyStamp<T>(this EntityTypeBuilder<T> builder, Expression<Func<T, ConcurrencyStamp>> propertyExpression) where T : class
        {
#pragma warning disable CS8620 // 由于引用类型的可为 null 性差异，实参不能用于形参。
            builder.OwnsOne(propertyExpression, sub => 
            {
                sub.Property(x => x.Token).IsRequired().HasMaxLength(32).IsConcurrencyToken().HasColumnName("ConcurrencyStamp");
            });
#pragma warning restore CS8620 // 由于引用类型的可为 null 性差异，实参不能用于形参。
        }

        public static void DeletionStatus<T>(this EntityTypeBuilder<T> builder, Expression<Func<T, DeletionStatus>> propertyExpression) where T : class
        {
#pragma warning disable CS8620 // 由于引用类型的可为 null 性差异，实参不能用于形参。
            builder.OwnsOne(propertyExpression, sub =>
            {
                sub.Property(x => x.MarkedForDeletion).IsRequired().HasColumnName("MarkedForDeletion");
            });
#pragma warning restore CS8620 // 由于引用类型的可为 null 性差异，实参不能用于形参。
        }
    }
}
