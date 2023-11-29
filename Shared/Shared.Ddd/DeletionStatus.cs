namespace Shared.Ddd
{
    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="MarkedForDeletion">标记是否被删除</param>
    public record class DeletionStatus(bool MarkedForDeletion)
    {
        public static readonly DeletionStatus Valid = new(false);
        public static readonly DeletionStatus Invalid = new(true);
    }
}
