namespace DataLayer.Mappings.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Country, InputCountryMapper>().ReverseMap();
            CreateMap<Country, EditCountryMapper>().ReverseMap();
            CreateMap<Country, CountryMapper>().ReverseMap();
        }
    }
}
