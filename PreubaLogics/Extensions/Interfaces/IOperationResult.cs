using System.Net;

namespace PreubaLogics.Extensions.Interfaces
{
    public interface IOperationResult
    {
        string Error { get; set; }

        string Message { get; set; }

        HttpStatusCode StatusCode { get; set; }

        bool Success { get; }

    }

    public interface IOperationResult<T> : IOperationResult

    {
        T? Result { get; }
    }

    public interface IOperationResultList<T> : IOperationResult
    {
        IEnumerable<T> Result { get; }
    }
}
