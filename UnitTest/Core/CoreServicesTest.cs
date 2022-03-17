namespace UnitTest.Core
{
    public abstract class CoreTest<TContext, TEntity, TInput,TEdit, TEntityVM>
        where TEntity : BaseModel, new()
        where TInput : BaseInputModel
        where TEntityVM : BaseDtoModel
        where TEdit : BaseEditModel
        where TContext : DbContext
    {
        protected IBaseRepository<TInput,TEdit,TEntityVM> _service;
        protected IMapper _mapper;
        protected List<TEntity> _data;
        protected TEntity _entity;
        protected TContext _dbContext;


        public Guid FakeId = Guid.NewGuid();

        protected abstract void Initialize();

        public IMapper SetUpMapper(Profile[] profiles)
        {
            var config = new MapperConfiguration(_ =>
            {
                foreach (var item in profiles)
                {
                    _.AddProfile(item);
                }
            });
            _mapper = config.CreateMapper();
            return _mapper;
        }

        public virtual List<TEntity> GetFakeDataList()
        {
            return _data;
        }

        public virtual TEntity GetFakeCreationData()
        {
            A.Configure<TEntity>().Fill(x => x.IsDeleted, () => false);
            return A.New<TEntity>();
        }

        public DbContextOptions<TContext> GetDbOptions()
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase("test")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return options;
        }

        [Fact]
        public async Task GetAllTest()
        {
            var result = await _service.GetList();
            Assert.True(result.Any());
        }

        [Fact]
        public virtual async Task GetByIdNotNullTest()
        {
            var result = await _service.GetById(_entity.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public virtual async Task GetByIdNull()
        {
            var result = await _service.GetById(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public virtual async Task SoftRemoveSuccessTest()
        {
            var en = await _dbContext.Set<TEntity>().ToListAsync();
            var result = await _service.SoftRemove(en[2].Id);
            Assert.True(result);
        }

        [Fact]
        public virtual async Task SoftRemoveFailTest()
        {
            var result = await _service.SoftRemove(Guid.NewGuid());
            Assert.True(!result);
        }

        [Fact]
        public virtual async Task AddSuccessTest()
        {
            var data = _mapper.Map<TEntity, TInput>(GetFakeCreationData());
            var result = await _service.Create(data);
            Assert.True(result != null);
        }

        [Fact]
        public virtual async Task UpdateSuccessTest()
        {
            var en = await _dbContext.Set<TEntity>().FirstOrDefaultAsync();
            var data = _mapper.Map<TEntity,TEdit>(en ?? new());
            var result = await _service.Update(data);
            Assert.True(result != null);
        }

        [Fact]
        public virtual async Task UpdateErrorTest()
        {
            var data = _mapper.Map<TEntity, TEdit>(_entity);
            data.Id = Guid.NewGuid();
            var result = await _service.Update(data);
            Assert.True(result == null);
        }

        [Fact]
        public virtual async Task RemoveFailTest()
        {
            var result = await _service.Remove(Guid.NewGuid());
            Assert.True(!result);
        }

        [Fact]
        public virtual async Task RemoveSuccessTest()
        {
            var en = await _dbContext.Set<TEntity>().FirstOrDefaultAsync();
            var result = await _service.Remove(en?.Id ?? Guid.NewGuid());
            Assert.True(result);
        }

    
    }
}
