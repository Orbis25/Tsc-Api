namespace DataLayer.Core
{
    public abstract class BaseDtoModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtStr { get; set; }
        public string UpdateAtStr { get; set; }

    }
}
