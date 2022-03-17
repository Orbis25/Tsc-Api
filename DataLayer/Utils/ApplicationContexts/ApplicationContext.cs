using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace DataLayer.Utils.ApplicationContexts
{
    public class ApplicationContext : IApplicationContext
    {
       public string UserName { get; set; } = string.Empty;

        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            IHeaderDictionary? headers = httpContextAccessor.HttpContext?.Request?.Headers;
            if(headers != null)
            {
                string token = headers["Authorization"].ToString().Replace("Bearer","").Trim();
                if(token != null)
                {
                    UserName = DecodeToken(token);
                }
            }
        }

        private string DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenInfo = jsonToken as JwtSecurityToken;
            return tokenInfo?.Claims?.First(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value ?? string.Empty;
        }
    }
}
