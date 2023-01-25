namespace DemoWebApi.Services.Interfaces
{
    interface IHashingService
    {
        byte[] GenerateHash(string s);
        byte[] GetHash(int id, string s);
        HashingSalt? GetSalt(int id);
    }
}
