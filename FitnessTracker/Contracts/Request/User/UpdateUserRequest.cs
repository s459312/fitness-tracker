using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Contracts.Request.User
{
    public class UpdateUserRequest
    {
        /// <summary>
        /// Nowe imię użytkownika
        /// </summary>
        /// <example>Example Name</example>
        public string Name { get; set; }
        
        /// <summary>
        /// Nowe nazwisko użytkownika
        /// </summary>
        /// <example>Example Name</example>
        public string Surname { get; set; }
    }
}