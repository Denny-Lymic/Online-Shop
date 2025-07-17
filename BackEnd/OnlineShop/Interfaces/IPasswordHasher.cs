namespace OnlineShop.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);

        bool VerifyHash(string password, string hash);
    }
}
