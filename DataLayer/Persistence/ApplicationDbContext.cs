namespace DataLayer.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //apply all ef configurations and filters
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEFConfiguration<>).Assembly);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
    }
}
