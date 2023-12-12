namespace SharedKernel.Exceptions;

/// <summary>
/// 业务异常
/// </summary>
public class BusinessException(string code) : Exception
{
    /// <summary>
    /// 异常代码
    /// </summary>
    public string Code { get; } = code;
}
