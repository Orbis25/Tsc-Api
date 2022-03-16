namespace Tsc.Api.Controllers.Countries
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : CoreController<ICountryService, CountryMapper, CountryInputMapper, CountryEditMapper>
    {
        public CountriesController(ICountryService service) : base(service)
        {

        }
    }
}
