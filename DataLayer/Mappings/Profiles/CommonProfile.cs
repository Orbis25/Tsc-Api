namespace DataLayer.Mappings.Profiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Country, CountryInputMapper>().ReverseMap();
            CreateMap<Country, CountryEditMapper>().ReverseMap();
            CreateMap<Country, CountryMapper>().ReverseMap();

            CreateMap<State, StateMapper>().ReverseMap();
            CreateMap<State, StateInputMapper>().ReverseMap();
            CreateMap<State, StateEditMapper>().ReverseMap();
        }
    }
}
