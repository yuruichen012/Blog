namespace Shared.Ddd
{
    /// <summary>
    /// 并发戳
    /// </summary>
    /// <param name="Token">标记</param>
    public record class ConcurrencyStamp(string Token)
    {
        /// <summary>
        /// 创建一个新并发戳
        /// </summary>
        /// <returns><see cref="ConcurrencyStamp"/></returns>
        public static ConcurrencyStamp Create() => new ConcurrencyStamp(Guid.NewGuid().ToString("N"));
    }
}
