

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

        /// <summary>
        /// Add a configuration of automapper
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void AddAutomapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommonProfile));
        }

        /// <summary>
        /// Inject the all services to serviceCollection
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
        }
    }
}
