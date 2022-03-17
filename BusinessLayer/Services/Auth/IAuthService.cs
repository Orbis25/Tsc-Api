namespace BusinessLayer.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Generate a token from user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        (bool, string) GenerateToken(AuthConfig model);
    }
}
