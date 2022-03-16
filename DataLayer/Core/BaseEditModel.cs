namespace DataLayer.Core
{
    public abstract class BaseEditModel
    {
        public Guid Id { get; set; }

        public DateTime UpdateAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
