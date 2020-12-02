namespace FitnessTracker.Contracts.Response.Auth
{
    public class AuthSuccessResponse
    {
        /// <summary>
        /// Token JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token do odświeżenia tokenu JWT bez podawania loginu i hasłą
        /// </summary>
        public string RefreshToken { get; set; }
    }
}