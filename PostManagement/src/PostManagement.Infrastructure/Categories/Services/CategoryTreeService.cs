using Microsoft.Extensions.DependencyInjection;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories.Data;
using PostManagement.UseCases.Categories.Services;
using SharedKernel;

namespace PostManagement.Infrastructure.Categories.Services;

public class CategoryTreeService(IServiceProvider serviceProvider) : ICategoryTreeService
{
    public CategoryTreeNodeDTO[] Roots { get; private set; } = [];
    public CategoryTreeNodeDTO[] Expands { get; private set; } = [];
    public CategoryTreeNodeDTO[] ExpandsWithoutChildren { get; private set; } = [];
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<int, Category>>();
        var categories = await repository.GetListAsync(cancellationToken);

        var expands = new List<CategoryTreeNodeDTO>(categories.Count);
        var expandsWithoutChildren = new List<CategoryTreeNodeDTO>(categories.Count);
        Roots = Structure(0, categories, expands);
        Expands = [.. expands];
        ExpandsWithoutChildren = Expands.Select(x => new CategoryTreeNodeDTO(x.Id, x.ParentId, x.Name, [])).ToArray();
        IsInitialized = true;

        CategoryTreeNodeDTO[] Structure(int currentId, List<Category> categories, List<CategoryTreeNodeDTO> expands)
        {
            var current = categories.Find(x => x.Id == currentId);
            var children = categories.Where(x => x.ParentId == currentId).ToList();
            if (children.Count != 0)
            {
                var childrenStructureds = children.SelectMany(x => Structure(x.Id, categories, expands)).ToArray();

                if (current == null)
                {
                    return childrenStructureds;
                }

                var structuredWithChildren = new CategoryTreeNodeDTO(current.Id, current.ParentId, current.Name, childrenStructureds);
                expands.Add(structuredWithChildren);
                return [structuredWithChildren];
            }

            if (current == null)
            {
                return [];
            }

            var structuredWithoutChildren = new CategoryTreeNodeDTO(current.Id, current.ParentId, current.Name, []);
            expands.Add(structuredWithoutChildren);

            return [structuredWithoutChildren];
        }
    }

    public async ValueTask<CategoryTreeNodeDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!IsInitialized)
        {
            await InitializeAsync(cancellationToken);
        }

        return Expands.FirstOrDefault(x => x.Id == id);
    }

    public async ValueTask<CategoryTreeNodeDTO?> GetCategoryWithoutChildrenByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!IsInitialized)
        {
            await InitializeAsync(cancellationToken);
        }

        return ExpandsWithoutChildren.FirstOrDefault(x => x.Id == id);
    }
}
