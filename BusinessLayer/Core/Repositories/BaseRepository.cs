namespace BussinesLayer.Core.Repositories
{
    /// <summary>
    /// Permit create the base logic of services
    /// </summary>
    /// <typeparam name="TContext">The class represent a dbcontext</typeparam>
    /// <typeparam name="TEntity">The entity class</typeparam>
    /// <typeparam name="TInputModel">Represent the class children of BaseInputModel </typeparam>
    /// <typeparam name="TEditModel">Represent the class children of BaseEditModel</typeparam>
    /// <typeparam name="TDtoModel">Represent the class children of BaseDtoModel</typeparam>
    public abstract class BaseRepository<TContext,TEntity, TInputModel,TEditModel, TDtoModel> :
        IBaseRepository<TInputModel,TEditModel, TDtoModel>
        where TContext : DbContext
        where TEntity : BaseModel
        where TInputModel : BaseInputModel
        where TDtoModel : BaseDtoModel
        where TEditModel : BaseEditModel
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        protected BaseRepository(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new DbUpdateException(ex.GetBaseException().Message);
            }
        }

        public virtual async Task<TDtoModel> Create(TInputModel model,CancellationToken cancellationToken = default)
        {
            var _model = _mapper.Map<TInputModel, TEntity>(model);

            _context.Set<TEntity>().Add(_model);

            await CommitAsync(cancellationToken);

            return _mapper.Map<TEntity, TDtoModel>(_model);
        }

        public virtual IQueryable<TDtoModel> GetAll(Expression<Func<TDtoModel, bool>> expression = null,
            bool orderDesc = true, Expression<Func<TDtoModel, object>> ordered = null
            , params Expression<Func<TDtoModel, object>>[] includes)
        {
            var results = _context.Set<TEntity>().ProjectTo<TDtoModel>(_mapper.ConfigurationProvider).AsQueryable();
            if (expression != null)
                results = results.Where(expression);

            foreach (var include in includes) results = results.Include(include);

            ///Order elements desc or asc
            if (ordered != null && orderDesc) results = results.OrderByDescending(ordered);

            if (!orderDesc && ordered != null) results = results.OrderBy(ordered);

            if (orderDesc) results = results.OrderByDescending(x => x.CreatedAt);
            else results = results.OrderBy(x => x.CreatedAt);

            return results;
        }

        public virtual async Task<PaginationResult<TDtoModel>> GetPaginatedList(Paginate paginate,
            Expression<Func<TDtoModel, bool>> expression = null,
            bool orderDesc = true,
            Expression<Func<TDtoModel, object>> ordered = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TDtoModel, object>>[] includes)
        {
            var results = GetAll(expression, orderDesc, ordered, includes);
            var total = results.Count();
            var pages = total / Paginate.GetQyt(paginate.Qyt);

            results = results.Skip((paginate.Page - 1) * paginate.Qyt).Take(paginate.Qyt);

            return new PaginationResult<TDtoModel>
            {
                ActualPage = paginate.Page,
                Qyt = paginate.Qyt,
                PageTotal = pages,
                Total = total,
                Results = await results.AsNoTracking().ToListAsync(cancellationToken)
            };

        }

        public virtual async Task<IEnumerable<TDtoModel>> GetList(Expression<Func<TDtoModel, bool>> expression = null,
            bool orderDesc = true,
            Expression<Func<TDtoModel, object>> ordered = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TDtoModel, object>>[] includes)
            => await GetAll(expression, orderDesc, ordered, includes).ToListAsync(cancellationToken);

        public virtual async Task<TDtoModel> Update(TEditModel model, CancellationToken cancellationToken = default)
        {

            var _model = _mapper.Map<TEditModel, TEntity>(model);
            _context.Set<TEntity>().Update(_model);

            await CommitAsync(cancellationToken);

            return _mapper.Map<TEntity, TDtoModel>(_model);

        }

        public virtual async Task<TDtoModel> GetById(Guid id,
            bool asNotTraking = false,
            CancellationToken cancellationToken = default,
            params Expression<Func<TDtoModel, object>>[] includes)
        {
            var results = GetAll(null, true, x => x.CreatedAt, includes);

            if (asNotTraking) results = results.AsNoTracking();

            return await results.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        }

        public virtual async Task<bool> SoftRemove(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TDtoModel, TEntity>(await GetById(id));

            if(entity == null)  return false;

            entity.IsDeleted = true;

            _context.Set<TEntity>().Update(entity);

            await CommitAsync(cancellationToken);

            return true;

        }

        public virtual async Task<bool> Remove(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TDtoModel, TEntity>(await GetById(id));

            if (entity == null) return false;

            _context.Set<TEntity>().Remove(entity);

            await CommitAsync(cancellationToken);

            return true;
        }

        public async Task<int> Count(CancellationToken cancellationToken = default, params Expression<Func<TDtoModel, bool>>[] expression)
        {
            var result = _context.Set<TEntity>().ProjectTo<TDtoModel>(_mapper.ConfigurationProvider)
                .AsQueryable();

            foreach (var item in expression) result = result.Where(item);

            return await result.CountAsync(cancellationToken);
        }

        public async Task<bool> Exist(Expression<Func<TDtoModel, bool>> expression = null, CancellationToken cancellationToken = default)
        {
            var result = _context.Set<TEntity>().ProjectTo<TDtoModel>(_mapper.ConfigurationProvider);
            return await result.AnyAsync(expression, cancellationToken);
        }
    }
}
