using AutoMapper;
using AutoMapper.QueryableExtensions;
using PreubaLogics.Extensions.Interfaces;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;

namespace PreubaLogics.Extensions.Operations
{
    public static class OperationExtensions
    {
        private static IMapper? _mapper;

        public static Assembly? Asembly { get; set; }

        public static void Initialize(IServiceProvider services)
        {
            _mapper = (IMapper)services.GetService(typeof(IMapper));
        }

        public static IOperationResultList<T> ToResultList<T>(this IEnumerable<T> entity,
                        int pageNumber = 1,
                        int pageSize = 10,
                        int count = 0)
        {
            return new OperationResultList<T>(HttpStatusCode.OK, default, entity, pageNumber, pageSize, count);
        }

        public static IOperationResultList<T> ToResultList<T>(this Exception ex, IEnumerable<T>? entity = null)
        {
            return new OperationResultList<T>(ex, entity);
        }

        public static IOperationResultList<T> ToResultList<T>(this IEnumerable<T> entity,
                        HttpStatusCode status,
                        string? message = default,
                        int pageNumber = 1,
                        int? pageSize = default,
                        int? count = default
                        )
        {
            return new OperationResultList<T>(status, message, entity, pageNumber, pageSize, count);
        }

        public static IOperationResultList<TResult> ToResultList<TQuery, TResult>(this IQueryable<TQuery> entity,
                            int pageNumber = 1,
                            int? pageSize = default)
        {
            IQueryable<TQuery> query = entity;
            int count = query.Count();

            if (pageSize > 0)
            {
                if (pageNumber > 1)
                {
                    query = query.Skip(pageSize.Value * (pageNumber - 1));
                }

                query = query.Take(pageSize.Value);
            }
            List<TResult> result = query
               .ProjectTo<TResult>(_mapper.ConfigurationProvider)
               .ToList();

            return new OperationResultList<TResult>(HttpStatusCode.OK, default, result, pageNumber, pageSize, count);
        }

        public static async Task<IOperationResultList<TResult>> ToResultList<TQuery, TResult>(
              this List<TQuery> entity,
              int pageNumber = 1,
              int? pageSize = default,
              Expression<Func<TResult, object>>? orderByDesc = null)
        {
            try
            {
                List<TQuery> query = entity;
                int count = query.Count();

                // Aplicar ordenamiento si se especificó

                List<TResult> result = _mapper.Map<List<TResult>>(query);

                if (orderByDesc != null)
                {
                    IQueryable<TResult> asquerabel = result.AsQueryable();
                    result = asquerabel.OrderByDescending(orderByDesc).ToList();
                }

                return await Task.FromResult(
                    new OperationResultList<TResult>(
                        HttpStatusCode.OK,
                        default,
                        result,
                        pageNumber,
                        pageSize,
                        count));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OperationResultList<TResult>(ex));
            }
        }

        public static async Task<IOperationResultList<TResult>> ToResultListAsync<TQuery, TResult>(this IOrderedQueryable<TQuery> entity,
                        int pageNumber = 1,
                        int? pageSize = default)
        {
            return await Task.FromResult(entity.ToResultList<TQuery, TResult>(pageNumber, pageSize));
        }

        public static async Task<IOperationResultList<TResult>> ToResultListAsync<TQuery, TResult>(this IQueryable<TQuery> entity,
                        int pageNumber = 1,
                        int? pageSize = default)
        {
            return await Task.FromResult(entity.ToResultList<TQuery, TResult>(pageNumber, pageSize));
        }

        public static async Task<IOperationResultList<TResult>> ToResultListAsync<TQuery, TResult>(this List<TQuery> entity,
                     int pageNumber = 1,
                     int? pageSize = default)
        {
            return await entity.ToResultList<TQuery, TResult>(pageNumber, pageSize);
        }

        public static async Task<IOperationResultList<T>> ToResultListAsync<T>(this IEnumerable<T> entity,
                        int pageNumber = 1,
                        int? pageSize = default,
                        int? count = default)
        {
            return await Task.FromResult(new OperationResultList<T>(HttpStatusCode.OK, default, entity, pageNumber, pageSize, count));
        }

        public static async Task<IOperationResultList<T>> ToResultListAsync<T>(this IEnumerable<T> entity,
                        HttpStatusCode status,
                        string? message = default,
                        int pageNumber = 1,
                        int? pageSize = default,
                        int? count = default)
        {
            return await Task.FromResult(new OperationResultList<T>(status, message, entity, pageNumber, pageSize, count));
        }

        public static IOperationResult<T> ToResult<T>(this T entity, HttpStatusCode status = HttpStatusCode.OK, string? message = default)
        {
            return new OperationResult<T>(status, message, entity);
        }

        public static async Task<IOperationResult<TResult>> ToResultAsync<TQuery,TResult>(this TQuery? entity)
            where TQuery : class
        {
            return await Task.FromResult(entity.ToResult<TQuery, TResult>());
        }

        public static IOperationResult<TResult> ToResult<TQuery, TResult>(this TQuery? entity, HttpStatusCode status = HttpStatusCode.OK, string? message = default, string? error = default)
        {
            try
            {
                TResult? result = _mapper.Map<TResult>(entity);
                return new OperationResult<TResult>(status, message, result, error);
            }
            catch (Exception ex)
            {
                return new OperationResult<TResult>(ex);
            }
        }

        public static IOperationResult ToResult(this Exception ex)
        {
            return new OperationResult(ex);
        }

        public static async Task<IOperationResultList<T>> ToResultListAsync<T>(this Exception ex)
        {
            return await Task.FromResult(new OperationResultList<T>(ex));
        }

        public static async Task<IOperationResult> ToResultAsync(this Exception ex)
        {
            return await Task.FromResult(new OperationResult(ex));
        }

        public static IOperationResultList<T> ToResultList<T>(this Exception ex) where T : class
        {
            return new OperationResultList<T>(ex);
        }

        public static IOperationResult<T> ToResult<T>(this Exception ex) where T : class
        {
            return new OperationResult<T>(ex);
        }

        public static async Task<IOperationResult<T>> ToResultAsync<T>(this Exception ex)
        {
            return await Task.FromResult(new OperationResult<T>(ex));
        }

        public static async Task<IOperationResult<T>> ToRequestAsync<T>(this T entity, HttpStatusCode status = HttpStatusCode.OK, string? message = default, string? error = default)
        {
            return await Task.FromResult(new OperationResult<T>(status, message, entity, error));
        }

        public static async Task<IOperationResult<T>> ToResultAsync<T>(this T entity, HttpStatusCode status = HttpStatusCode.OK, string? message = default, string? error = default)
        {
            return await Task.FromResult(new OperationResult<T>(status, message, entity, error));
        }

        public static DateTime? ToDateTime(this string date)
        {
            if (!string.IsNullOrWhiteSpace(date))
            {
                if (DateTime.TryParse(date, CultureInfo.GetCultureInfo("es"), DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            return default;
        }
    }
}