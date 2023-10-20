using System.Net;

namespace PreubaLogics.Extensions
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public HttpStatusCode Code { get; set; }
        public string? Message { get; set; }
        public string? Errors { get; set; }

        public Result(bool succeeded, HttpStatusCode code, string? message, string? errors)
        {
            Succeeded = succeeded;
            Code = code;
            Message = message;
            Errors = errors;
        }
    }

    public class Result<T> : Result
        where T : class
    {
        public T? Data { get; set; }
        public Result(bool succeeded, HttpStatusCode code, string? message, string? errors) : base(succeeded, code, message, errors)
        {
        }
        public Result(T data, bool succeeded, HttpStatusCode code, string? message, string? errors) : base(succeeded, code, message, errors)
        {
            Data = data;
        }


    }
}
