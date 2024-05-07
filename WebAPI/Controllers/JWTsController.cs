using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Viewmodels.Books;
using WebAPI.Viewmodels.GenrateToken;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ReservationDevContext _context;

        public AuthController(IConfiguration configuration, ReservationDevContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UsersLogins obj)
        {
            var user = (from a in _context.People
                        join b in _context.Roles on a.RoleId equals b.Id
                        where a.PersonCode == obj.PersonCode && a.Password == obj.Password
                        select new UsersPerson
                        {
                            PersonName = a.PersonName,
                            PersonCode = a.PersonCode,
                            Password = a.Password,
                            Name = b.Name
                        }).FirstOrDefault();
            //var user = _context.People.FirstOrDefault(o => o.PersonCode == obj.PersonCode && o.Password == obj.Password);

            if (user != null)
            {

                var token = Generate(user);
                
                return Ok(token);
            }

            return NotFound("User not found");
        }


        private string Generate(UsersPerson user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            DateTime expirationTime = DateTime.Now.AddMinutes(15);
            var userId = _context.People.FirstOrDefault(a => a.PersonCode == user.PersonCode);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.PersonName),
                new Claim(ClaimTypes.Email, user.PersonCode),
                new Claim(ClaimTypes.Role, user.Name)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: expirationTime,
              signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            //var tokenRecord = new Token
            //{
            //    Tokenkey = tokenString,
            //    Status = true,
            //    Date = DateTime.Now,
            //    Exp = expirationTime,
            //    PersonId = userId.PersonId
            //};

            //_context.Tokens.Add(tokenRecord);
            //_context.SaveChanges();

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private async Task Authenticate(UsersLogins userLogin)
        //{
        //    var currentUser = _context.People.FirstOrDefault(o => o.PersonCode == userLogin.PersonCode && o.Password == userLogin.Password);

        //    if (currentUser != null)
        //    {
        //        return _await currentUser;
        //    }

        //    return null;
        //}
    }
}

