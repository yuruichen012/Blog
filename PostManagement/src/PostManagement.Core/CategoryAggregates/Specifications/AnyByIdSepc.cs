using Ardalis.Specification;

namespace PostManagement.Core.CategoryAggregates.Specifications;

public class AnyByIdSepc : Specification<Category>
{
    public AnyByIdSepc(uint id)
    {
        Query.Where(x => x.Id == id && x.DeletionStatus.MarkedForDeletion == false);
    }
}
