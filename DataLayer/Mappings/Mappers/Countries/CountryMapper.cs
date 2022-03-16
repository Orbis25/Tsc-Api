namespace DataLayer.Mappings.Mappers
{
    public class CountryMapper : BaseDtoModel
    {
        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string NumberCode { get; set; }

        public List<StateMapper> States { get; set; }
    }
}
