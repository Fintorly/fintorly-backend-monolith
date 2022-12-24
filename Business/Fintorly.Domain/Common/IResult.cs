using System;
namespace Fintorly.Domain.Common
{
    public interface IResult
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public object Data { get; set; }
    }
    public interface IResult<T>
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public object Data { get; set; }
    }
}

