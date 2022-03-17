namespace Tsc.Api.Controllers.State
{
    [Route("api/[controller]")]
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

        /// <summary>
        /// add a new
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
    }
}
