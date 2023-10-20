using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PreubaLogics.Extensions.Interfaces;
using PreubaLogics.Extensions.Operations;
using System.Net;

namespace Persona.Api.CommonHttp
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected static IConfiguration? Settings { get; private set; }
        protected static IMapper? _mapper { get; private set; }
        protected static IWebHostEnvironment? HostingEnvironment { get; private set; }
        protected string ResourcesPath => Settings["Resources"];

        public static void Initialize(IServiceProvider services, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            Settings = config;
            HostingEnvironment = hostingEnvironment;
            _mapper = services.GetService<IMapper>();
        }

        public string Ip => RequestIP?.ToString() ?? "";

        public DateTime Fecha => DateTime.UtcNow;


        public IPAddress RequestIP
        {
            get
            {
                IPAddress? remoteIpAddress = HttpContext.Connection?.RemoteIpAddress;

                return remoteIpAddress;
            }
        }

        //
        // Summary:
        //     Creates a Microsoft.AspNetCore.Mvc.ContentResult object by specifying a HTML template file and model
        //
        // Parameters:
        //   content:
        //     The content to write to the response.
        //
        //   contentType:
        //     The content type (MIME type).
        //
        // Returns:
        //     The created Microsoft.AspNetCore.Mvc.ContentResult object for the response.


        [NonAction]
        protected virtual ObjectResult Error(Exception ex, string? message = null)
        {
            OperationResult result = new(ex)
            {
                Message = message ?? ex.Message
            };

            return StatusCode(500, result);
        }

        [NonAction]
        protected virtual ObjectResult Error(HttpStatusCode statusCode, string message, string exception)
        {
            return StatusCode((int)statusCode, new OperationResult(statusCode, message, exception));
        }

        [NonAction]
        protected virtual ObjectResult Error(IOperationResult result)
        {
            return StatusCode((int)result.StatusCode, result);
        }

        [NonAction]
        protected virtual ObjectResult StatusCode(HttpStatusCode statusCode, object result)
        {
            return StatusCode((int)statusCode, result);
        }

        [NonAction]
        protected virtual ObjectResult StatusCode<T>(IOperationResult<T> result)
        {
            return StatusCode((int)result.StatusCode, result);
        }
    }


    public static class Extensions
    {
        public static ObjectResult ToObjectResult<T>(this IOperationResult<T> result) where T : class
        {
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        public static ObjectResult ToObjectResult(this IOperationResult result)
        {
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        public static ObjectResult ToObjectResult(this Exception ex, string? message = null)
        {
            IOperationResult result = ex.ToResult();
            if (!string.IsNullOrEmpty(message))
            {
                result.Message = message;
            }

            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }
    }
}
