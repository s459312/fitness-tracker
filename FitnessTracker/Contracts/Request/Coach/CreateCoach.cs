namespace FitnessTracker.Contracts.Request.Coach
{

    public class CreateCoach
    {

        /// <summary>
        /// Imię trenera
        /// </summary>
        /// <example>Janusz</example>
        public string Name { get; set; }

        /// <summary>
        /// Nazwisko trenera
        /// </summary>
        /// <example>Tracz</example>
        public string Surname { get; set; }

        /// <summary>
        /// Adres poczty elektronicznej trenera
        /// </summary>
        /// <example>rectorof@amu.edu.pl</example>
        public string Email { get; set; }

        /// <summary>
        /// Telefon trenera
        /// </summary>
        /// <example>601100100</example>
        public string Phone { get; set; }

        /// <summary>
        /// Klucz podstawowy obiektu celu trenera
        /// </summary>
        /// <example>1</example>
        public int GoalId { get; set; }

    }

}
