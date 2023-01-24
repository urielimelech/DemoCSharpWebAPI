using AlbarWebAPI.Data.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AlbarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private void stringToHash(string str, out byte[] salt, out byte[] hash)
        {
            using(var hmac = new HMACSHA256())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            }
        }

        private bool verifyUserPassowrd(string str, User u) {
            using (var hmac = new HMACSHA256(u.Salt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
                return hash.SequenceEqual(u.Password);
            }
        }

        private string CreateJWToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWT_Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<SigninObj> Register(SigninObj user)
        {
            if(user.Password.IsNullOrEmpty() || user.Email.IsNullOrEmpty()) {
                throw new ArgumentException("empty password or email was entered");
            }
            try
            {
                var existUser = _context.Users.Where(u => u.Email == user.Email);
                if (existUser.IsNullOrEmpty())
                {
                    byte[] salt = new byte[32];
                    byte[] hash = new byte[32];
                    stringToHash(user.Password, out salt, out hash);
                    User dtoUser = new User(user.Email, salt, hash);

                    _context.Users.Add(dtoUser);
                    _context.SaveChanges();
                    return Ok();
                }
                throw new DuplicateNameException("User exist");
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<SigninObj> Login(SigninObj user)
        {
            try
            {
                User existUser = _context.Users.Where(u=>u.Email == user.Email).First();
                if (verifyUserPassowrd(user.Password, existUser))
                {
                    return Ok(CreateJWToken(existUser));
                }
                throw new IOException("Invalid credetials. Please try again");
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
