namespace API_Authentication_BasicImplementation.Models
{
    public class TokenResponseDTO
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
