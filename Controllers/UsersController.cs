using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        // access configuration (appsettings)
        private readonly IConfiguration _config;
        public UsersController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            User userExists = _db.Users.SingleOrDefault(x => x.Email == user.Email);
            if (userExists != null)
            {
                return BadRequest("The email already exists");
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentUser = _db.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            // if the user not found
            if (currentUser == null)
            {
                return NotFound();
            }
            // if the user founded ( Generate the JWT Token )
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            // Hashing the key
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            // custom claims returned in the payload
            var Claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            // Generate JWT
            var Token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: Claims,
                signingCredentials: Credentials,
                expires: DateTime.Now.AddMinutes(60)
                );
            // Get the Token in a string format
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(Token);
            return Ok(JwtToken);
        }
    }
}
