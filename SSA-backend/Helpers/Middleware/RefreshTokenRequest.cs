using System.Text.Json.Serialization;

namespace SSA.backend.Helpers.Middleware
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
