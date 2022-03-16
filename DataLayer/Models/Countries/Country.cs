namespace DataLayer.Models
{
    public class Country : BaseModel
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

        public ICollection<State> States { get; set; }

        public static List<string> OrderByOptions
        {
            get => new()
            {
                nameof(Name),
                nameof(Alpha2Code),
                nameof(Alpha3Code),
                nameof(Id),
                nameof(CreatedAt),
                nameof(CreatedBy),
                nameof(NumberCode)
            };
        }

    }
}
