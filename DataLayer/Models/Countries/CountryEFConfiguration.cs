
namespace DataLayer.Models.Countries
{
    public class CountryEFConfiguration : BaseEFConfiguration<Country>
    {
        public override void ConfigureEF(EntityTypeBuilder<Country> builder)
        {
            builder.HasIndex(x => x.Name).HasFilter("IsDeleted = 0").IsUnique();
            builder.HasIndex(x => x.NumberCode).HasFilter("IsDeleted = 0").IsUnique();
            builder.HasIndex(x => x.Alpha2Code).HasFilter("IsDeleted = 0").IsUnique();
            builder.HasIndex(x => x.Alpha3Code).HasFilter("IsDeleted = 0").IsUnique();
        }
    }
}
