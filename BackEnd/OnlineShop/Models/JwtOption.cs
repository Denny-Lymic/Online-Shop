namespace BackEnd.Models
{
    public class JwtOption
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpirationHours { get; set; }
    }
}