namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthConfig _authConfig;
        private readonly JwtConfig _jwtConfig;
        public AuthService(IOptions<AuthConfig> authConfig, IOptions<JwtConfig> jwtConfig)
        {
            _authConfig = authConfig.Value;
            _jwtConfig = jwtConfig.Value;
        }

        public (bool, string) GenerateToken(AuthConfig model)
        {
            if (model.User == _authConfig.User && model.Password == _authConfig.Password)
            {
                List<Claim> claims = GetClaims(model);
                return (true, new JwtSecurityTokenHandler().WriteToken(BuildToken(claims)));
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// Build a token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private JwtSecurityToken BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            signingCredentials: creds);

            return token;
        }

        /// <summary>
        /// get the claims
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<Claim> GetClaims(AuthConfig model)
        {
            return new()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.User),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }


    }
}
