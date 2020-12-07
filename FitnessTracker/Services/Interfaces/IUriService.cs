using FitnessTracker.Contracts.Request.Queries;
using System;

namespace FitnessTracker.Services.Interfaces
{
    public interface IUriService
    {
        Uri CreatePaginationRequestUrl(PaginationQuery paginationQuery = null);

        string GetBaseUri();
    }
}