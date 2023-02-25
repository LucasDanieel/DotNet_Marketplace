using DotNet.Marketplace.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public static class PagedBaseResponseHelper
    {
        public static async Task<TResponse> Pagination<TResponse, T>(IQueryable<T> query, PagedBaseRequest request)
            where TResponse : PagedBaseResponse<T>, new()
        {
            var response = new TResponse();
            var count = await query.CountAsync();
            response.TotalRegister = count;
            response.TotalPages = count / request.PageSize;

            if (string.IsNullOrEmpty(request.OrderByProperty))
                response.Data = query.ToList();
            else
                response.Data = query.OrderByDynamic(request.OrderByProperty)
                                    .Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            return response;
        }

        private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string property)
        {
            return query.OrderBy(x => x.GetType().GetProperty(property).GetValue(x, null)).ToList();
        }
    }
}
