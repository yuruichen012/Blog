using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostManagement.Infrastructure.EntityFrameworkCore;

namespace PostManagement.Infrastructure;

public static class PostManagementInfrastructureExtensions
{
    public static void AddPostManagementDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PostManagementDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}
