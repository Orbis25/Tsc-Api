namespace BusinessLayer.Services
{
    public class StateService : BaseRepository<ApplicationDbContext, State, StateInputMapper, StateEditMapper,StateMapper>
        , IStateService
    {
        public StateService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
