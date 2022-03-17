namespace DataLayer.Utils.Configs
{
    public class AuthConfig
    {
        [Required]
        [EmailAddress]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
