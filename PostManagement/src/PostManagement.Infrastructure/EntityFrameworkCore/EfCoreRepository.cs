using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class EfCoreRepository<T>(PostManagementDbContext context) : RepositoryBase<T>(context), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
