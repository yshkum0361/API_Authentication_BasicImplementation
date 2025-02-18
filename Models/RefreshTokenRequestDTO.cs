namespace API_Authentication_BasicImplementation.Models
{
    public class RefreshTokenRequestDTO
    {
        public Guid UserId { get; set; }

        public required string RefreshToken { get; set; }
    }
}
