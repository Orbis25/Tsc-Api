namespace DataLayer.Core
{
    public abstract class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdateAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyy hh:mm:ss");

        [NotMapped]
        public string UpdateAtStr => CreatedAt.ToString("dd/MM/yyyy hh:mm:ss");

        public bool IsDeleted { get; set; }
        
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
