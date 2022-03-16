namespace Tsc.Api.Configurations
{
    public static class ProgramConfiguration
    {
        /// <summary>
        /// Represent a dbContext configuration
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }
    }
}
