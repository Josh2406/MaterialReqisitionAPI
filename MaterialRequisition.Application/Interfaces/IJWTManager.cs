namespace MaterialRequisition.Application.Interfaces
{
    public interface IJWTManager
    {
        /// <summary>
        /// Generate JSON Web Token (JWT) with authentic username
        /// </summary>
        /// <param name="username">Unique Username</param>
        /// <param name="expirationInMinutes">Optional Token Expiry Time in Minutes</param>
        /// <returns>JWT (<see cref="string"/>)</returns>
        string GenerateToken(string username, int expirationInMinutes = 30);

        /// <summary>
        /// Get the username of currently authenticated user
        /// </summary>
        /// <returns>Username (<see cref="string"/>)</returns>
        string GetCurrentlyLoggedInUsername();
    }
}
