using DemoWebAPI.Data.Objects;
using DemoWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DemoWebApi.Services;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private bool verifyUserPassowrd(string str,int id, byte[] hashedPassword)
        {
            HashingService hashingService = new HashingService(_context);
            byte[] hash = hashingService.GetHash(id, str);
            return hash.SequenceEqual(hashedPassword);
        }

        [HttpPost("register")]
        public ActionResult<SigninObj> Register(SigninObj request)
        {
            if(request.Password.IsNullOrEmpty() || request.Email.IsNullOrEmpty()) {
                throw new ArgumentException("empty password or email was entered");
            }
            try
            {
                var existUser = _context.Users.Where(u => u.Email == request.Email);
                if (existUser.IsNullOrEmpty())
                {
                    HashingService hashingService = new HashingService(_context);
                    byte[] hash = hashingService.GenerateHash(request.Password);
                    User dtoUser = new User(request.Email, hash);

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
        public ActionResult<SigninObj> Login(SigninObj request)
        {
            try
            {
                User existUser = _context.Users.Where(u=>u.Email == request.Email).First();
                if (verifyUserPassowrd(request.Password, existUser.Id, existUser.Password))
                {
                    JsonWebTokenService jwt = new(_configuration);
                    return Ok(jwt.GenerateJWToken(existUser.Email));
                }
                throw new IOException("Invalid credetials. Please try again");
            }
            catch (Exception e)
            {
                return Unauthorized($"User is not exist, {e.Message}");
            }
        }

        [HttpPost("abc")]
        public ActionResult<string> Abc()
        {
            return Ok();
        }
    }
}
