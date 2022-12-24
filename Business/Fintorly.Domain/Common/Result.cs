using Fintorly.Domain.Enums;

namespace Fintorly.Domain.Common
{
    public class Result : IResult
    {
        public string Message { get; set; } 
        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public ResultStatus ResultStatus { get; set; }

        public static IResult Fail()
        {
            return new Result { ResultStatus = ResultStatus.Error, Succeeded = false };
        }
        public static IResult Fail(string message)
        {
            return new Result { ResultStatus = ResultStatus.Error, Succeeded = false, Message = message };
        }
        public static IResult Success()
        {
            return new Result { ResultStatus = ResultStatus.Success, Succeeded = true };
        }
        public static IResult Success(object data)
        {
            return new Result { ResultStatus = ResultStatus.Success, Succeeded = true,Data=data };
        }
        public static IResult Success(string message)
        {
            return new Result { ResultStatus = ResultStatus.Success, Succeeded = true, Message = message };
        }
        public static IResult Success(string message, object data)
        {
            return new Result { ResultStatus = ResultStatus.Success, Succeeded = true, Message = message, Data = data };
        }
    }
    
    public class Result<T> : IResult<T>
    {
        public string Message { get; set; } 
        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public ResultStatus ResultStatus { get; set; }

        public static IResult<T> Fail()
        {
            return new Result<T> { ResultStatus = ResultStatus.Error, Succeeded = false };
        }
        public static IResult<T> Fail(string message)
        {
            return new Result<T> { ResultStatus = ResultStatus.Error, Succeeded = false, Message = message };
        }
        public static IResult<T> Success(T data)
        {
            return new Result<T> { ResultStatus = ResultStatus.Success, Succeeded = true, Data = data };
        }
        public static IResult<T> Success()
        {
            return new Result<T> { ResultStatus = ResultStatus.Success, Succeeded = true };
        }
        public static IResult<T> Success(string message)
        {
            return new Result<T> { ResultStatus = ResultStatus.Success, Succeeded = true, Message = message };
        }
        public static IResult<T> Success(string message, T data)
        {
            return new Result<T> { ResultStatus = ResultStatus.Success, Succeeded = true, Message = message, Data = data };
        }
    }
    
}