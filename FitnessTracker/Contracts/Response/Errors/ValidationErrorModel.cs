using System.Collections.Generic;

namespace FitnessTracker.Contracts.Response.Errors
{
    public class ValidationErrorModel
    {
        public string FieldName { get; set; }

        public List<string> Errors { get; set; }
    }
}