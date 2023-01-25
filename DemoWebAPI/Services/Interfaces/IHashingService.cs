namespace DemoWebApi.Services.Interfaces
{
    interface IHashingService
    {
        byte[] GetHash(string s);
    }
}
