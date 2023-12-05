using MediatR;

namespace PostManagement.Core.CategoryAggregates;

public record class CategoryParentChangedDomainEvent(Category Category) : INotification;
