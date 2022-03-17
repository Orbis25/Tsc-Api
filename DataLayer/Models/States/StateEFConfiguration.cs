namespace DataLayer.Models.States
{
    public class StateEFConfiguration : BaseEFConfiguration<State>
    {
        public override void ConfigureEF(EntityTypeBuilder<State> builder)
        {
            builder.HasIndex(x => x.Name).HasFilter("IsDeleted = 0").IsUnique();
        }
    }
}
