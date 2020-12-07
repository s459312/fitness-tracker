using FitnessTracker.Contracts.Request.Queries;
using FitnessTracker.Contracts.Response;
using FitnessTracker.Models.Filters;
using FitnessTracker.Services.Interfaces;
using System.Collections.Generic;

namespace FitnessTracker.Helpers
{
    public class PaginationHelper
    {
        public static int CountSkip(PaginationFilter filter)
        {
            return (filter.PageNumber - 1) * filter.PageSize;
        }

        public static PaginationQuery ValidateQuery(PaginationQuery query)
        {
            query.PageNumber = (query.PageNumber < 1) ? 1 : query.PageNumber;
            query.PageSize = (query.PageSize < 1) ? 1 : query.PageSize;
            query.PageSize = (query.PageSize > 30) ? 30 : query.PageSize;

            return query;
        }

        public static PagedResponse<T> Paginate<T>(IUriService uriService, PaginationFilter pagination, List<T> response, int total = 0)
        {
            int pageSize = pagination.PageSize >= 1 ? pagination.PageSize : 1;
            pageSize = (pageSize > 30) ? 30 : pageSize;
            int totalPages = (total + pageSize - 1) / pageSize;

            int pageNumber;
            if (pagination.PageNumber > totalPages)
            {
                pageNumber = totalPages;
            }
            else
            {
                pageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : 1;
            }

            bool createPreviousPage = pagination.PageNumber - 1 >= 1 && pagination.PageNumber <= totalPages;

            var nextPage = pageNumber < totalPages
                ? uriService
                    .CreatePaginationRequestUrl(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;

            var previousPage = createPreviousPage
                ? uriService
                    .CreatePaginationRequestUrl(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString()
                : null;

            var firstPage = uriService
                .CreatePaginationRequestUrl(new PaginationQuery(1, pagination.PageSize)).ToString();

            var lastPage = uriService
                .CreatePaginationRequestUrl(new PaginationQuery(totalPages, pagination.PageSize)).ToString();


            return new PagedResponse<T>
            {
                Data = response,
                Meta = new PagedResponseMeta
                {
                    Total = total,
                    TotalPages = totalPages,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    NextPage = nextPage,
                    PreviousPage = previousPage,
                    FirstPage = firstPage,
                    LastPage = lastPage
                }
            };
        }
    }
}