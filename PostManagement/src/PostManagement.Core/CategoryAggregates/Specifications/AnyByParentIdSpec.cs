using Ardalis.Specification;

namespace PostManagement.Core.CategoryAggregates.Specifications;

public class AnyByParentIdSpec : Specification<Category>
{
    public AnyByParentIdSpec(uint parentId)
    {
        Query.Where(x => x.ParentId == parentId && x.DeletionStatus.MarkedForDeletion == false);
    }
}
