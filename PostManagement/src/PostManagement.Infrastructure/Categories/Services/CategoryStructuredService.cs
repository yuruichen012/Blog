using System.Collections.Immutable;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PostManagement.Core.CategoryAggregates;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.UseCases.Categories.Data;
using PostManagement.UseCases.Categories.Services;
using SharedKernel;

namespace PostManagement.Infrastructure.Categories.Services;

public class CategoryStructuredService : ICategoryStructuredService
    , INotificationHandler<CategoryCreatedDomainEvent>
{
    public IServiceProvider ServiceProvider { get; }
    public CategoryStructuredDTO[] Roots { get; private set; } = [];
    public CategoryStructuredDTO[] Expands { get; private set; } = [];
    public CategoryStructuredDTO[] ExpandsWithoutChildren { get; private set; } = [];

    public CategoryStructuredService(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        using var scope = ServiceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<int, Category>>();
        var categories = await repository.GetListAsync(cancellationToken);

        var expands = new List<CategoryStructuredDTO>(categories.Count);
        var expandsWithoutChildren = new List<CategoryStructuredDTO>(categories.Count);
        Roots = Structure(0, categories, expands);
        Expands = [.. expands];
        ExpandsWithoutChildren = Expands.Select(x => new CategoryStructuredDTO(x.Id, x.ParentId, x.Name, [])).ToArray();

        CategoryStructuredDTO[] Structure(int currentId, List<Category> categories, List<CategoryStructuredDTO> expands)
        {
            var current = categories.Find(x => x.Id == currentId);
            var children = categories.Where(x => x.ParentId == currentId).ToList();
            if (children.Count != 0)
            {
                var childrenStructureds = children.SelectMany(x => Structure(x.Id, categories, expands)).ToArray();

                return current == null
                    ? childrenStructureds
                    : [new CategoryStructuredDTO(current.Id, current.ParentId, current.Name, childrenStructureds)];
            }

            if (current == null)
            {
                return [];
            }

            var structured = new CategoryStructuredDTO(current.Id, current.ParentId, current.Name, []);
            expands.Add(structured);

            return [structured];
        }
    }

    public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await InitializeAsync(cancellationToken);
    }

    public ValueTask<CategoryStructuredDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(Expands.FirstOrDefault(x => x.Id == id));
    }

    public ValueTask<CategoryStructuredDTO?> GetCategoryWithoutChildrenByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(ExpandsWithoutChildren.FirstOrDefault(x => x.Id == id));
    }
}
