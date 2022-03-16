namespace Tsc.Api.Controllers.Countries
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : CoreController<ICountryService, CountryMapper, InputCountryMapper, EditCountryMapper>
    {
        public CountryController(ICountryService service) : base(service)
        {

        }
    }
}
