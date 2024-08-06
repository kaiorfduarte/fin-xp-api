using FinXp.Domain.Enum;
using System.Runtime.Serialization;
using System.Text;

namespace FinXp.Domain.Util;

[DataContract]
public class ServiceResult
{
    [DataMember]
    public bool Success { get; set; }

    public Exception Error { get; set; }

    private readonly StringBuilder Info = new StringBuilder();

    [DataMember]
    public string ErrorMessage
    {
        get
        {
            return Error?.Message;
        }
        set { }
    }

    public ServiceResult SetAditionalInfo(string message)
    {
        Info.AppendLine(message);
        return this;
    }

    public string GetAditionalInfo()
    {
        return Info.ToString();
    }

    [DataMember]
    public ServiceResultErrorType ErrorType
    {
        get;
        set;
    }

    public ServiceResult SetSuccess()
    {
        Success = true;
        Error = null;
        return this;
    }

    public ServiceResult SetError(Exception ex)
    {
        Success = false;
        Error = ex;
        if (ex is ApplicationException)
        {
            ErrorType = ServiceResultErrorType.Application;
        }
        else
        {
            ErrorType = ServiceResultErrorType.Exception;
        }
        return this;
    }

    public ServiceResult SetError(string errorMessage)
    {
        Success = false;
        SetError(new ApplicationException(errorMessage));
        return this;
    }

    public ServiceResult SetError(string errorMessage, params object[] parameters)
    {
        SetError(string.Format(errorMessage, parameters));
        return this;
    }

    public static ServiceResult GetSuccessResult()
    {
        var result = new ServiceResult();
        return result.SetSuccess();
    }

    public static ServiceResult GetErrorResult(string errorMessage)
    {
        var result = new ServiceResult();
        return result.SetError(errorMessage);
    }
}

[DataContract]
public class ServiceResult<T> : ServiceResult
{
    [DataMember]
    public T Data { get; set; }

    public new ServiceResult<T> SetSuccess()
    {
        base.SetSuccess();
        return this;
    }

    public ServiceResult<T> SetSuccess(T result)
    {
        base.SetSuccess();
        Data = result;
        return this;
    }

    public new ServiceResult<T> SetError(Exception ex)
    {
        base.SetError(ex);
        Data = default;
        return this;
    }

    public new ServiceResult<T> SetError(string errorMessage)
    {
        base.SetError(errorMessage);
        return this;
    }

    public new ServiceResult<T> SetError(string errorMessage, params object[] parameters)
    {
        base.SetError(errorMessage, parameters);
        return this;
    }

    public static ServiceResult<T> GetSuccessResult(T result)
    {
        var returnResult = new ServiceResult<T>();
        return returnResult.SetSuccess(result);
    }
    public static ServiceResult<T> GetErrorResult(string errorMessage)
    {
        var result = new ServiceResult<T>();
        return result.SetError(errorMessage);
    }
}