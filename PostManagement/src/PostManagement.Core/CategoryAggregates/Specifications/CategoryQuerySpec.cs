using System.Linq.Expressions;
using Ardalis.Specification;

namespace PostManagement.Core.CategoryAggregates.Specifications;

public class CategoryQuerySpec<TResult> : Specification<Category, TResult> where TResult : class
{
    public CategoryQuerySpec(Expression<Func<Category, TResult>> selector, Guid? parentId = null, string? name = null, bool nameLikeQuery = true, int? skip = null, int? take = null)
    {
        Query.Select(selector);

        if (parentId.HasValue)
        {
            Query.Where(x => x.ParentId == parentId);
        }

        if (name != null)
        {
            if (nameLikeQuery)
            {
                Query.Where(x => x.Name.Contains(name));
            }
            else
            {
                Query.Where(x => x.Name == name);
            }
        }

        if (skip.HasValue)
        {
            Query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            Query.Take(take.Value);
        }
    }
}
