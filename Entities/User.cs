namespace API_Authentication_BasicImplementation.Entities
{
    public class User
    {
        public Guid id { get; set; }
        public String UserName { get; set; } = string.Empty;

        public String PasswordHash { get; set; } = string.Empty;    

        public String Role { get; set; }

        public string? RefreshToken  { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
