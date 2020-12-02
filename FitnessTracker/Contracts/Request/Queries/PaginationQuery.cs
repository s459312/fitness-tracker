namespace FitnessTracker.Contracts.Request.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize  > 30 ? 30 : pageSize;
        }

        /// <summary>
        /// Numer strony
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; }
        
        /// <summary>
        /// Ilość elementów na stronie
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; }
    }
}