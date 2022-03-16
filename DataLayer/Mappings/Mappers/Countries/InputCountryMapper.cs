namespace DataLayer.Mappings.Mappers
{
    public class InputCountryMapper : BaseInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string Alpha2Code { get; set; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string Alpha3Code { get; set; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string NumberCode { get; set; }
    }
}
