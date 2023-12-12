namespace SharedKernel.Exceptions;

/// <summary>
/// 对象未找到异常
/// </summary>
public class ObjectNotFoundException(string code) : BusinessException(code)
{
}
