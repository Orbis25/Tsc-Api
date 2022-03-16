namespace Tsc.Api.Controllers.State
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : CoreController<IStateService, StateMapper, StateInputMapper, StateEditMapper>
    {
        private readonly ICountryService _countryService;
        public StatesController(IStateService service, ICountryService countryService) : base(service)
        {
            _countryService = countryService;
        }

        [HttpPost]
        public override async Task<IActionResult> Create(StateInputMapper inputModel, CancellationToken cancellationToken = default)
        {
            var existCountry = await _countryService.Exist(x => x.Id == inputModel.CountryId, cancellationToken);

            if (!existCountry)
                return BadRequest($"The country id : {inputModel.CountryId} not was found");

            return await base.Create(inputModel, cancellationToken);
        }
    }
}
