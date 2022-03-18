using Tsc.Api.Controllers.State;

namespace UnitTest.Controllers
{
    public class StateControllerTest
    {
        private readonly Mock<IStateService> _stateServiceMock = new();
        private readonly Mock<ICountryService> _countryServiceMock = new();

        public StateControllerTest()
        {
            A.Set<StateInputMapper>();
            A.Set<StateEditMapper>();
            A.Set<PaginationResult<StateMapper>>();
        }


        [Fact]
        public async Task GetByCountryWork()
        {
            var list = A.New<PaginationResult<StateMapper>>();

            _stateServiceMock.Setup(x => x.GetPaginatedList(It.IsAny<Paginate>(),
                It.IsAny<Expression<Func<StateMapper, bool>>>(),
                It.IsAny<Expression<Func<StateMapper, object>>>(), default))
                .ReturnsAsync(list);

            var controller = new StatesController(_stateServiceMock.Object, _countryServiceMock.Object);

            var result = await controller.Get(Guid.NewGuid(), new(), default) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateWithInvalidCountryId()
        {
            var model = A.New<StateInputMapper>();

            var controller = new StatesController(_stateServiceMock.Object, _countryServiceMock.Object);

            var result = await controller.Create(model, default) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task CreateWithExistEntity()
        {
            var model = A.New<StateInputMapper>();

            _stateServiceMock.Setup(x => x.Exist(It.IsAny<Expression<Func<StateMapper, bool>>>(), default))
                          .ReturnsAsync(true);

            var controller = new StatesController(_stateServiceMock.Object, _countryServiceMock.Object);

            var result = await controller.Create(model, default) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}
