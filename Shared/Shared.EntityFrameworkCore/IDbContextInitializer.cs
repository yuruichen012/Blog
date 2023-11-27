namespace Shared.EntityFrameworkCore
{
    /// <summary>
    /// 数据上下文初始化器
    /// </summary>
    public interface IDbContextInitializer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Task InitializeAsync();
    }
}
