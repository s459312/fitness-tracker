using System;
using FitnessTracker.Contracts.Request.Queries;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace FitnessTracker.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public string GetBaseUri()
        {
            return _baseUri;
        }
        
        public Uri CreatePaginationRequestUrl(PaginationQuery paginationQuery = null)
        {
            var uri = new Uri(_baseUri);

            if (paginationQuery == null)
                return uri;
            
            var modifiedUrl =
                QueryHelpers.AddQueryString(uri.ToString(), "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUrl = QueryHelpers.AddQueryString(modifiedUrl, "pageSize", paginationQuery.PageSize.ToString());
            return new Uri(modifiedUrl);
        }
    }
}