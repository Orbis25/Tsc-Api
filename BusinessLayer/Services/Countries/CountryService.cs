namespace BusinessLayer.Services
{
    public class CountryService :
        BaseRepository<ApplicationDbContext, Country, CountryInputMapper, CountryEditMapper, CountryMapper>
        , ICountryService
    {

        public CountryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<PaginationResult<CountryMapper>> GetPaginatedList(Paginate paginate, Expression<Func<CountryMapper, bool>> expression = null, Expression<Func<CountryMapper, object>> ordered = null, CancellationToken cancellationToken = default, params Expression<Func<CountryMapper, object>>[] includes)
        {
            if (!string.IsNullOrEmpty(paginate.OrderBy) && Country.OrderByOptions.Any(prop => prop == paginate.OrderBy))
                ordered = GetOrderByProperty(paginate.OrderBy);

            if(!string.IsNullOrEmpty(paginate.Query))
                expression = where => where.Name.Contains(paginate.Query) || where.Alpha2Code.Contains(paginate.Query);

            var results = await base.GetPaginatedList(paginate, expression, ordered, cancellationToken, includes);

            results.OrderOptions = Country.OrderByOptions;

            return results;
        }

        private Expression<Func<CountryMapper, object>> GetOrderByProperty(string prop)
        {
            return prop switch
            {
                nameof(Country.Name) => x => x.Name,
                nameof(Country.Alpha2Code) => x => x.Alpha3Code,
                nameof(Country.Alpha3Code) => x => x.Alpha3Code,
                nameof(Country.Id) => x => x.Id,
                nameof(Country.CreatedBy) => x => x.CreatedBy,
                nameof(Country.NumberCode) => x => x.NumberCode,
                _ => x => x.CreatedAt,
            };
        }

    }
}
