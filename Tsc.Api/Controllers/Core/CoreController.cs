namespace Tsc.Api.Controllers.Core
{
    /// <summary>
    /// Controller base for the others controllers
    /// </summary>
    /// <typeparam name="TService">The service children fron IBaseRepository</typeparam>
    /// <typeparam name="TDtoModel">Represent the class children of BaseDtoModel</typeparam>
    /// <typeparam name="TInputModel">Represent the class children of BaseInputModel </typeparam>
    /// <typeparam name="TEditModel">Represent the class children of BaseEditModel</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class CoreController<TService, TDtoModel, TInputModel, TEditModel> : ControllerBase
        , ICoreController<TInputModel, TEditModel>
        where TInputModel : BaseInputModel
        where TDtoModel : BaseDtoModel
        where TEditModel : BaseEditModel
        where TService : IBaseRepository<TInputModel, TEditModel, TDtoModel>
    {
        private readonly TService _service;
        protected CoreController(TService service) => _service = service;

       
        [HttpPost]
        public virtual async Task<IActionResult> Create(TInputModel inputModel, CancellationToken cancellationToken = default)
        {
            return Created(nameof(GetById), await _service.Create(inputModel, cancellationToken));
        }

        /// <summary>
        /// Remove a entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public virtual async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _service.SoftRemove(id, cancellationToken);

            if (!result)
                return NotFound(id);

            return Ok(result);
        }

        /// <summary>
        /// Get all entities paginated
        /// </summary>
        /// <param name="paginate">Paginated model</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] Paginate paginate,
            CancellationToken cancellationToken = default)
        {
            return Ok(await _service.GetPaginatedList(paginate));
        }


        /// <summary>
        /// Get a entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public virtual async Task<IActionResult> GetById(Guid id,
            CancellationToken cancellationToken = default)
        {
            var response = await _service.GetById(id, cancellationToken: cancellationToken);
            if (response == null) return NotFound("Not Found");
            return Ok(response);
        }

        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="editModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<IActionResult> Update(TEditModel editModel,
            CancellationToken cancellationToken = default)
        {
            var exist = await _service.Exist(x => x.Id == editModel.Id, cancellationToken);
            if (!exist)
                return NotFound(exist);

            var response = await _service.Update(editModel, cancellationToken);

            if (response == null) return BadRequest(response);

            return Ok(response);
        }
    }
}
