namespace Tsc.Api.Controllers.State
{
    [Route("api/states")]
    [ApiController]
    public class StatesController : CoreController<IStateService, StateMapper, StateInputMapper, StateEditMapper>
    {
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        public StatesController(IStateService service, ICountryService countryService) : base(service)
        {
            _countryService = countryService;
            _stateService = service;
        }

        [HttpPost]
        public override async Task<IActionResult> Create(StateInputMapper inputModel, CancellationToken cancellationToken = default)
        {
            var existCountry = await _countryService.Exist(x => x.Id == inputModel.CountryId, cancellationToken);

            if (!existCountry)
                return BadRequest($"The country id : {inputModel.CountryId} not was found");

            var entityExist = await _stateService.Exist(x => x.Name == inputModel.Name || x.Code == inputModel.Code, cancellationToken);

            if (entityExist)
                return BadRequest("The entity exist in the db");

            return await base.Create(inputModel, cancellationToken);
        }

        [HttpGet("country/{id:guid}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery] Paginate paginate, CancellationToken cancellationToken = default)
        {
            return Ok(await _stateService.GetPaginatedList(paginate, x => x.CountryId == id, cancellationToken: cancellationToken));
        }
    }
}
