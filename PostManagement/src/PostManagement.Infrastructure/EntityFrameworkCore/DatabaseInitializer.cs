using Microsoft.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class DatabaseInitializer(PostManagementDbContext dbContext)
{
    public virtual void Initialize()
    {
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
    }
}
