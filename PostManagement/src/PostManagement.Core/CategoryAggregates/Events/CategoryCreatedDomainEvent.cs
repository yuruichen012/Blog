using MediatR;

namespace PostManagement.Core.CategoryAggregates.Events;

public record class CategoryCreatedDomainEvent(Category Category) : INotification;
