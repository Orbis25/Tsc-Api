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
            services.AddScoped<IStateService, StateService>();
        }

        /// <summary>
        /// cors configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AnyOrigin", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
