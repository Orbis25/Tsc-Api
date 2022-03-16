namespace DataLayer.Models
{
    public class State : BaseModel
    {
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public static List<string> OrderByOptions
        {
            get => new()
            {
                nameof(Code),
                nameof(Name),
            };
        }
    }
}
