using DemoWebApi.Services.Interfaces;
using DemoWebAPI.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace DemoWebApi.Services
{
    public class HashingService : IHashingService
    {
        private readonly DataContext _context;
        public HashingService(DataContext context)
        {
            _context = context;
        }

        public byte[] GenerateHash(string s)
        {
            using (var hmac = new HMACSHA256())
            {
                byte[] saltKey = hmac.Key;
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(s));
                _context.HashingSalts.Add(new HashingSalt(saltKey));
                _context.SaveChanges();
                return hash;
            }
        }

        public byte[] GetHash(int id, string s)
        {
            HashingSalt? salt = GetSalt(id);
            using (var hmac = new HMACSHA256())
            {
                if (salt != null)
                {
                    hmac.Key = salt.salt;
                    return hmac.ComputeHash(Encoding.UTF8.GetBytes(s));
                }
                throw new InvalidDataException();
            }
        }
        public HashingSalt? GetSalt(int id)
        {
            try
            {
                return _context.HashingSalts.Where(s => s.id == id).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
