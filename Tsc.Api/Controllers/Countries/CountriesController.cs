namespace Tsc.Api.Controllers.Countries
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : CoreController<ICountryService, CountryMapper, CountryInputMapper, CountryEditMapper>
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService service) : base(service)
        {
            _countryService = service;
        }

        /// <summary>
        /// add a new
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]   
        public override async Task<IActionResult> Create(CountryInputMapper inputModel, CancellationToken cancellationToken = default)
        {
            var existOne = await _countryService.Exist(x => x.Name == inputModel.Name 
            || x.Alpha3Code == inputModel.Alpha3Code
            || x.Alpha2Code == inputModel.Alpha2Code
            || x.NumberCode == inputModel.NumberCode,cancellationToken);

            if (existOne)
                return BadRequest("The entity exist, all property are unique");

            return await base.Create(inputModel, cancellationToken);
        }
    }
}
