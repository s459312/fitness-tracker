namespace FitnessTracker.Contracts.Request.Auth
{
    public class AuthRefreshTokenRequest
    {
        /// <summary>
        /// Stary token JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token do odświeżenia tokenu JWT bez podawania loginu i hasłą
        /// </summary>
        public string RefreshToken { get; set; }
    }
}