namespace DataLayer.Mappings.Mappers
{
    public class StateInputMapper : BaseInputModel
    {
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Guid CountryId { get; set; }
    }
}
