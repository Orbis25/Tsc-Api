
namespace DataLayer.Core
{
    public abstract class BaseInputModel
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }


        [JsonIgnore]
        public string? CreatedBy { get; set; }

    }
}
