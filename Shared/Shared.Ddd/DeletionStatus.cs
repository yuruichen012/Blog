using Ardalis.SharedKernel;

namespace Shared.Ddd
{
    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="markedForDeletion">标记是否被删除</param>
    public class DeletionStatus(bool markedForDeletion) : ValueObject
    {
        public static readonly DeletionStatus Valid = new(false);
        public static readonly DeletionStatus Invalid = new(true);

        public bool MarkedForDeletion { get; init; } = markedForDeletion;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MarkedForDeletion;
        }
    }
}
