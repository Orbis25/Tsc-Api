using DataLayer.Utils.Configs;
using Microsoft.Extensions.Options;

namespace UnitTest.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IOptions<AuthConfig>> _authConfig = new();
        private readonly Mock<IOptions<JwtConfig>> _jwtConfig = new();


        public AuthServiceTest()
        {
            _authConfig.Setup(x => x.Value)
                .Returns(new AuthConfig() { User = "test@domain.com", Password = "abc123" });
            _jwtConfig.Setup(x => x.Value).Returns(new JwtConfig()
            {
                Audience = "localhost.com",
                Issuer = "localhost.com",
                SecretKey = "sdjskdajskdjasdasd2aw1q09d1q51d08s0a515a1s98d4a9s1da501da1s098das051da5s1d98a1s5d1as51d9as4d5a1s5d1a9sd01asd980sa10das"
            });
        }

        [Fact]
        public void LoginSuccess()
        {
            var service = new AuthService(_authConfig.Object, _jwtConfig.Object);

            (bool status, string token) = service.GenerateToken(new()
            {
                User = "test@domain.com",
                Password = "abc123"
            });

            Assert.NotNull(token);
            Assert.True(status);
        }

        [Fact]
        public void LoginInvalid()
        {
            var service = new AuthService(_authConfig.Object, _jwtConfig.Object);

            (bool status, string token) = service.GenerateToken(new()
            {
                User = "testx@domain.com",
                Password = "abc123"
            });

            Assert.True(string.IsNullOrEmpty(token));
            Assert.False(status);
        }
    }
}
