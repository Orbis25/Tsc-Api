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
            services.AddScoped<IApplicationContext, ApplicationContext>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IAuthService, AuthService>();
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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                     },
                     Array.Empty<string>()
                   }
                  });
            });
        }

        /// <summary>
        /// Add a jwt initial configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection("JwtConfig");

            //for this test not allow a expiration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = section["Issuer"],
                   ValidAudience = section["Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["SecretKey"])),
                   ClockSkew = TimeSpan.Zero
               });
        }

        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthConfig>(configuration.GetSection(nameof(AuthConfig)));
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
        }
    }
}
