using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore.Initializers;

public class PostManagementDbContextInitializer(PostManagementDbContext context) : IDbContextInitializer
{
    public async Task InitializeAsync()
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}
