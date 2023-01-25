namespace DemoWebApi.Services.Interfaces
{
    public interface IJsonWebTokenService
    {
        string GenerateJWToken(string user);
    }
}
