namespace UnitTest.Services
{
    public class CountryServiceTest : CoreServicesTest<ApplicationDbContext, Country, CountryInputMapper, CountryEditMapper, CountryMapper>
    {
        private ApplicationDbContext _context;
        private readonly Mock<IApplicationContext> _applicationContextMock = new();

        public CountryServiceTest()
        {
            A.Set<State>().Fill(x => x.CountryId, () => Guid.Empty);
            A.Set<Country>()
                .Fill(x => x.IsDeleted, () => false)
                .Fill(x => x.States, () => A.ListOf<State>(5));
            _applicationContextMock.Setup(x => x.UserName).Returns("test");

            Initialize();

        }

        protected override void Initialize()
        {
            A.Configure<Country>().Fill(x => x.IsDeleted, () => false);

            _data = A.ListOf<Country>(10);
            _entity = _data.First();

            _context = new(GetDbOptions(), _applicationContextMock.Object);
            _context.AddRange(GetFakeDataList());
            _context.SaveChanges();

            var profiles = new Profile[] { new CommonProfile() };
            _mapper = SetUpMapper(profiles);
            _service = new CountryService(_context, _mapper);
            _dbContext = _context;
        }

        [Fact]
        public async Task GetPaginatedSuccess()
        {
            ApplicationDbContext db = new(GetDbOptions(), _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new CountryService(db, _mapper);

            var results = await service.GetPaginatedList(new());

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetPaginatedWithOrderingSuccess()
        {
            ApplicationDbContext db = new(GetDbOptions(), _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new CountryService(db, _mapper);

            var results = await service.GetPaginatedList(new() { OrderBy = nameof(Country.Name) });

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetPaginatedWithQueryParamSuccess()
        {
            ApplicationDbContext db = new(GetDbOptions(), _applicationContextMock.Object);
            db.Countries.AddRange(A.ListOf<Country>(20));

            var service = new CountryService(db, _mapper);

            var results = await service.GetPaginatedList(new() { OrderBy = nameof(Country.Name), Query = "x" });

            Assert.NotNull(results);
        }

        public override async Task SoftRemoveSuccessTest()
        {
            var list = A.ListOf<Country>(20);

            ApplicationDbContext db = new(GetDbOptions(), _applicationContextMock.Object);
            await db.Countries.AddRangeAsync(list);
            await db.SaveChangesAsync();

            var entity = await db.Countries.AsNoTracking().FirstOrDefaultAsync();
            var service = new CountryService(db, _mapper);

            var results = await service.SoftRemove(entity?.Id ?? Guid.NewGuid());
            Assert.True(results);

        }
    }
}
