namespace BusinessLayer.Services
{
    public class StateService : BaseRepository<ApplicationDbContext, State, StateInputMapper, StateEditMapper,StateMapper>
        , IStateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public StateService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public override async Task<PaginationResult<StateMapper>> GetPaginatedList(Paginate paginate, Expression<Func<StateMapper, bool>> expression = null, Expression<Func<StateMapper, object>> ordered = null, CancellationToken cancellationToken = default, params Expression<Func<StateMapper, object>>[] includes)
        {
            if (!string.IsNullOrEmpty(paginate.OrderBy) && State.OrderByOptions.Any(prop => prop == paginate.OrderBy))
                ordered = GetOrderByProperty(paginate.OrderBy);

            if (!string.IsNullOrEmpty(paginate.Query))
                expression = where => where.Name.Contains(paginate.Query) || where.Code.Contains(paginate.Query);

            var results = await base.GetPaginatedList(paginate, expression, ordered, cancellationToken, includes);

            results.OrderOptions = State.OrderByOptions;

            return results;
        }


        private Expression<Func<StateMapper, object>> GetOrderByProperty(string prop)
        {
            return prop switch
            {
                nameof(State.Name) => x => x.Name,
                nameof(State.Code) => x => x.Id,
                _ => x => x.CreatedAt,
            };
        }
    }
}
