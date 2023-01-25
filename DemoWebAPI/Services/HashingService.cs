using DemoWebApi.Services.Interfaces;
using DemoWebAPI.Data;
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
        public byte[] GetHash(string s)
        {
            HashingSalt salt = GetSalt();
            using (var hmac = new HMACSHA256())
            {
                byte[]? saltKey = salt == null ? hmac.Key : salt.salt;
                if (salt == null)
                {
                    _context.HashingSalts.Add(new HashingSalt(saltKey));
                    _context.SaveChanges();
                }
                hmac.Key = saltKey;
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(s));
            }
        }
        public HashingSalt? GetSalt()
        {
            try
            {
                return _context.HashingSalts.First();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
