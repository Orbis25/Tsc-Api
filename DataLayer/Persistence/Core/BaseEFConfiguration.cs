
namespace DataLayer.Persistence.Core
{
    /// <summary>
    /// Represent a ef configuration base
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEFConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : BaseModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
            ConfigureEF(builder);
        }

        public abstract void ConfigureEF(EntityTypeBuilder<TEntity> builder);
    }
}
