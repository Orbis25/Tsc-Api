namespace UnitTest.Services
{
    public class StateServiceTest 
    {

        private readonly Mock<IApplicationContext> _applicationContextMock = new();
        private readonly DbContextOptions<ApplicationDbContext> _dbOptions;
        private readonly IMapper _mapper;
        
        
        public StateServiceTest()
        {
            A.Set<State>();
            _applicationContextMock.Setup(x => x.UserName).Returns("test");

            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("test-state")
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;

            var profiles = new Profile[] { new CommonProfile() };

            var config = new MapperConfiguration(_ =>
            {
                foreach (var item in profiles)
                {
                    _.AddProfile(item);
                }
            });

            _mapper = config.CreateMapper();
        }

      


        [Fact]
        public async Task GetPaginatedSuccess()
        {
            ApplicationDbContext db = new(_dbOptions, _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new StateService(db, _mapper);

            var results = await service.GetPaginatedList(new());

            Assert.NotNull(results);
        }


        [Fact]
        public async Task GetPaginatedWithOrderingSuccess()
        {
            ApplicationDbContext db = new(_dbOptions, _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new StateService(db, _mapper);

            var results = await service.GetPaginatedList(new() { OrderBy = nameof(Country.Name) });

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetPaginatedWithQueryParamSuccess()
        {
            ApplicationDbContext db = new(_dbOptions, _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new StateService(db, _mapper);

            var results = await service.GetPaginatedList(new() { OrderBy = nameof(Country.Name), Query = "x" });

            Assert.NotNull(results);
        }

    }
}
