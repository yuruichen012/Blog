using Ardalis.Specification;

namespace PostManagement.Core.CategoryAggregates.Specifications;

public class CategoryByIdSepc : Specification<Category>
{
    public CategoryByIdSepc(uint id)
    {
        Query.Where(x => x.Id == id && x.DeletionStatus.MarkedForDeletion == false);
    }
}
