namespace BusinessLayer.Services
{
    public class CountryService :
        BaseRepository<ApplicationDbContext, Country, InputCountryMapper, EditCountryMapper, CountryMapper>
        , ICountryService
    {
        public CountryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
