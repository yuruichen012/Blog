using Ardalis.SharedKernel;

namespace Shared.Ddd
{
    /// <summary>
    /// 并发戳
    /// </summary>
    /// <param name="token">标记</param>
    public class ConcurrencyStamp(string token) : ValueObject
    {
        private bool isNew = false;

        public string Token { get; private set; } = token;

        public void Change()
        {
            if (isNew)
            {
                return;
            }

            isNew = true;
            Token = Guid.NewGuid().ToString("N");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
        }

        /// <summary>
        /// 创建一个新并发戳
        /// </summary>
        /// <returns><see cref="ConcurrencyStamp"/></returns>
        public static ConcurrencyStamp Create() => new(Guid.NewGuid().ToString("N"));
    }
}
