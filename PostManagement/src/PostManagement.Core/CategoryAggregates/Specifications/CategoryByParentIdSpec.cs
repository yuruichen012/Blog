using Ardalis.Specification;

namespace PostManagement.Core.CategoryAggregates.Specifications;

public class CategoryByParentIdSpec : Specification<Category>
{
    public CategoryByParentIdSpec(uint parentId)
    {
        Query.Where(x => x.ParentId == parentId && x.DeletionStatus.MarkedForDeletion == false);
    }
}
