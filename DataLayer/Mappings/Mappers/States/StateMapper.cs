namespace DataLayer.Mappings.Mappers
{
    public class StateMapper : BaseDtoModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public CountryMapper Country { get; set; }
    }
}
