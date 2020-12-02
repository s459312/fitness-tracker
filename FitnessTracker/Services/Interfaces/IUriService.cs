using System;
using FitnessTracker.Contracts.Request.Queries;

namespace FitnessTracker.Services.Interfaces
{
    public interface IUriService
    {
        Uri CreatePaginationRequestUrl(PaginationQuery paginationQuery = null);
        
        string GetBaseUri();
    }
}