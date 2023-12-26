using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore;

/// <summary>
/// 身份认证数据上下文
/// </summary>
public class IdentityManagementDbContext(DbContextOptions<IdentityManagementDbContext> options) : DbContext(options)
{
}
