namespace UnitTest.Controllers
{
    public class CountryControllerTest
    {
        private readonly Mock<ICountryService> _countryServiceMock = new();

        public CountryControllerTest()
        {
            A.Set<CountryInputMapper>();
            A.Set<CountryEditMapper>();
            A.Set<PaginationResult<CountryMapper>>();
        }


        [Fact]
        public async Task CreateSuccess()
        {
            var country = A.New<CountryInputMapper>();
            var mapped = new CountryMapper()
            {
                Alpha2Code = country.Alpha2Code,
                Alpha3Code = country.Alpha3Code,
                NumberCode = country.NumberCode,
                Name = country.Name,
            };

            _countryServiceMock.Setup(x => x.Create(It.IsAny<CountryInputMapper>(), default))
                .ReturnsAsync(mapped);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Create(country) as CreatedResult;

            Assert.IsType<CreatedResult>(result);
            Assert.Equal(country.Alpha2Code, mapped.Alpha2Code);

        }

        [Fact]
        public async Task CreateErrorExistOne()
        {
            _countryServiceMock.Setup(x => x.Exist(It.IsAny<Expression<Func<CountryMapper, bool>>>(), default))
               .ReturnsAsync(true);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Create(new()) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task UpdatedSuccess()
        {
            var country = A.New<CountryEditMapper>();
            var mapped = new CountryMapper()
            {
                Alpha2Code = country.Alpha2Code,
                Alpha3Code = country.Alpha3Code,
                NumberCode = country.NumberCode,
                Name = country.Name,
            };

            _countryServiceMock.Setup(x => x.Exist(It.IsAny<Expression<Func<CountryMapper, bool>>>(), default))
                .ReturnsAsync(true);

            _countryServiceMock.Setup(x => x.Update(It.IsAny<CountryEditMapper>(), default))
                .ReturnsAsync(mapped);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Update(country) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(country.Alpha2Code, mapped.Alpha2Code);

        }

        [Fact]
        public async Task UpdatedFailedNotFoundId()
        {
            var country = A.New<CountryEditMapper>();
            var mapped = new CountryMapper()
            {
                Alpha2Code = country.Alpha2Code,
                Alpha3Code = country.Alpha3Code,
                NumberCode = country.NumberCode,
                Name = country.Name,
            };

            _countryServiceMock.Setup(x => x.Update(It.IsAny<CountryEditMapper>(), default))
                .ReturnsAsync(mapped);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Update(country) as NotFoundObjectResult;

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdatedError()
        {

            _countryServiceMock.Setup(x => x.Exist(It.IsAny<Expression<Func<CountryMapper, bool>>>(), default))
                .ReturnsAsync(true);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Update(new()) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAllSuccess()
        {
            var list = A.New<PaginationResult<CountryMapper>>();


            _countryServiceMock.Setup(x => x.GetPaginatedList(It.IsAny<Paginate>(),
                It.IsAny<Expression<Func<CountryMapper, bool>>>(), 
                It.IsAny<Expression<Func<CountryMapper, object>>>(), default))
                .ReturnsAsync(list);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.GetAll(new()) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdSuccess()
        {
            var entity = A.New<CountryMapper>();


            _countryServiceMock.Setup(x => x.GetById(It.IsAny<Guid>(),It.IsAny<bool>(), default))
                .ReturnsAsync(entity);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.GetById(Guid.NewGuid()) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.GetById(Guid.NewGuid()) as NotFoundObjectResult;

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteSuccess()
        {
            _countryServiceMock.Setup(x => x.SoftRemove(It.IsAny<Guid>(), default))
                .ReturnsAsync(true);

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Delete(Guid.NewGuid()) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task DeleteNotFound()
        {

            var controller = new CountriesController(_countryServiceMock.Object);

            var result = await controller.Delete(Guid.NewGuid()) as NotFoundObjectResult;

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
