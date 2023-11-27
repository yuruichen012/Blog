using Microsoft.EntityFrameworkCore;
using Shared.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore.Data;

public class PostManagementDbContextInitializer(PostManagementDbContext context) : IDbContextInitializer
{
    public async Task InitializeAsync()
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();
    }
}
