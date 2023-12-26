using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore;

public class DatabaseInitializer(IdentityManagementDbContext dbContext)
{
    public virtual void Initialize()
    {
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
    }
}
