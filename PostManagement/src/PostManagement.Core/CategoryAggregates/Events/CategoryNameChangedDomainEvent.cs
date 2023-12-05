using MediatR;

namespace PostManagement.Core.CategoryAggregates.Events;

public record class CategoryNameChangedDomainEvent(Category Category) : INotification;
