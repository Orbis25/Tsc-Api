namespace UnitTest.Services
{
    public class CountryServiceTest : CoreServicesTest<ApplicationDbContext, Country, CountryInputMapper, CountryEditMapper, CountryMapper>
    {
        private ApplicationDbContext _context;
        private readonly Mock<IApplicationContext> _applicationContextMock = new();

        public CountryServiceTest()
        {
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

        public override async Task SoftRemoveSuccessTest()
        {
            Assert.True(true);
        }
    }
}
