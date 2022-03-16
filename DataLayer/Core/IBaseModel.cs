namespace DataLayer.Core
{
    public interface IBaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreatedAtStr { get; }
        public string UpdateAtStr { get; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
