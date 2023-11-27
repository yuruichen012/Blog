using Microsoft.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

/// <summary>
/// PostManagement 数据上下文
/// </summary>
/// <param name="options">配置选项</param>
public class PostManagementDbContext(DbContextOptions<PostManagementDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostManagementInfrastructureModule).Assembly);
    }
}
