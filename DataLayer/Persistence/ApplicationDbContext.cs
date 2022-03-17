namespace DataLayer.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IApplicationContext _applicationContext;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IApplicationContext applicationContext) : base(options)
        {
            _applicationContext = applicationContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //apply all ef configurations and filters
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEFConfiguration<>).Assembly);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<BaseModel>())
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.UpdateAt = DateTime.Now;
                        entity.Entity.UpdatedBy = _applicationContext.UserName;
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.Now;
                        entity.Entity.CreatedBy = _applicationContext.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
    }
}
