namespace FitnessTracker.Contracts.Response
{
    public class PagedResponseMeta
    {
    #pragma warning disable 1570
        /// <summary>
        /// Całkowita ilość elementów
        /// </summary>
        /// <example>100</example>
        public int Total { get; set; }
        
        /// <summary>
        /// Ilość ston
        /// </summary>
        /// <example>10</example>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Numer aktualnej strony
        /// </summary>
        /// <example>2</example>
        public int? PageNumber { get; set; }
        
        /// <summary>
        /// Ilość elementów wyświetlanych na stronie
        /// </summary>
        /// <example>10</example>
        public int? PageSize { get; set; }
        
        /// <summary>
        /// Link do następnej strony
        /// </summary>
        /// <example>https://example.com/posts?PageNumber=3&PageSize=10</example>
        public string NextPage { get; set; }
        
        /// <summary>
        /// Link do poprzedniej strony
        /// </summary>
        /// <example>https://example.com/posts?PageNumber=1&PageSize=10</example>
        public string PreviousPage { get; set; }
        
        /// <summary>
        /// Link do pierwszej strony
        /// </summary>
        /// <example>https://example.com/posts?PageNumber=1&PageSize=10</example>

        public string FirstPage { get; set; }
        
        /// <summary>
        /// Link do poprzedniej strony
        /// </summary>
        /// <example>https://example.com/posts?PageNumber=10&PageSize=10</example>
        public string LastPage { get; set; }
        
        #pragma warning restore 1570
    }
}