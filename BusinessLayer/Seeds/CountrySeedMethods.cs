using Newtonsoft.Json;

namespace BusinessLayer.Seeds
{
    public static class CountrySeedMethods
    {
        public static void PoblateDatabase(this IApplicationBuilder app, string enviroment)
        {
            using IServiceScope? scoped = app.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope();

            IMapper? mapped = scoped?.ServiceProvider?.GetService<IMapper>();

            using ApplicationDbContext? context = scoped?.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null && mapped != null)
            {
                bool existAny = context.Countries.Any();

                if (!existAny)
                {
                    List<CountrySeed> list = GetFromFile(enviroment);
                    Poblate(context, list, mapped);
                }
            }
        }

        private static void Poblate(ApplicationDbContext context, List<CountrySeed> list, IMapper mapper)
        {
            List<Country> mapped = mapper.Map<List<Country>>(list);
            context.Countries.AddRange(mapped);
            context.SaveChanges();
        }

        private static List<CountrySeed> GetFromFile(string rootPath)
        {
            List<CountrySeed> items;
            string path = $@"{rootPath}//Resources//data.json";

            using (StreamReader st = new(path))
            {
                string json = st.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CountrySeed>>(json) ?? new();
            }

            return items;
        }
    }
}
