namespace BusinessLayer.Services
{
    public class CountryService :
        BaseRepository<ApplicationDbContext, Country, CountryInputMapper, CountryEditMapper, CountryMapper>
        , ICountryService
    {
        public CountryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
