namespace UnitTest.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authServiceMock = new();

        public AuthControllerTest()
        {

        }

        [Fact]
        public void LoginSuccess()
        {
            _authServiceMock.Setup(x => x.GenerateToken(It.IsAny<AuthConfig>()))
                .Returns((true, "token"));


            var controller = new AuthController(_authServiceMock.Object);

            var result = controller.Login(new()) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);

        }
    }
}
