
namespace SharedKernel.Common;

public class PageHelper
{
    /// <summary>
    /// 获取跳过数量
    /// </summary>
    public static int GetSkip(int pageNumber, int pageSize)
    {
        return (pageNumber - 1) * pageSize;
    }

    /// <summary>
    /// 获取跳过数量
    /// </summary>
    public static long GetSkip(long pageNumber, long pageSize)
    {
        return (pageNumber - 1) * pageSize;
    }

    /// <summary>
    /// 获取总页数
    /// </summary>
    public static int GetTotalPages(int pageSize, int recordCount)
    {
        var totalPages = recordCount / pageSize;
        return recordCount % pageSize == 0 ? totalPages : totalPages + 1;
    }

    /// <summary>
    /// 获取总页数
    /// </summary>
    public static long GetTotalPages(long pageSize, long recordCount)
    {
        var totalPages = recordCount / pageSize;
        return recordCount % pageSize == 0 ? totalPages : totalPages + 1;
    }
}
