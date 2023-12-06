using Microsoft.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class PostManagementDbContext(DbContextOptions<PostManagementDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostManagementDbContext).Assembly);
    }
}
