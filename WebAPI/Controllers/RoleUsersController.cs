using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Models;
using WebAPI.Viewmodels.GenrateToken;
using WebAPI.Viewmodels.Persons;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]

    public class RoleUsersController : ControllerBase
    {
        private readonly ReservationDevContext _context;

        public RoleUsersController(ReservationDevContext context)
        {
            _context = context;
        }

        [HttpPost("Admins")]
        [Authorize(Roles = "Admins")]
        public IActionResult AdminsEndpoint()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var currentUser = GetCurrentUser(token);
            var user = _context.People.FirstOrDefault(o => o.PersonCode == currentUser.PersonCode);
            UsersData personResults = new UsersData();
            if (user != null)
            {
                var person = (from a in _context.People
                              join b in _context.Roles on a.RoleId equals b.Id
                              where a.PersonCode == user.PersonCode && a.Password == user.Password
                              select new UsersPerson
                              {
                                  PersonId = a.PersonId,
                                  PersonCode = a.PersonCode,
                                  Password = a.Password,
                                  PersonName = a.PersonName,
                                  RoleId = b.Id,
                                  Name = b.Name
                              }).FirstOrDefault();

                if (person == null)
                {
                    personResults.UsersPersons = null;

                }
                else
                {
                    personResults.UsersPersons = person;

                }
            }
            return Ok(personResults);
        }
        private UsersPerson GetCurrentUser(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                if (expClaim != null && long.TryParse(expClaim, out long expUnixTime))
                {
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expUnixTime).UtcDateTime;

                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        var userClaims = identity.Claims;

                        UsersPerson user = new UsersPerson
                        {
                            PersonName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                            PersonCode = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
                            //Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                        };

                        var userRoles = _context.People.FirstOrDefault(u => u.PersonCode == user.PersonCode);
                        if (userRoles != null)
                        {
                            var tokenRecord = new Token
                            {
                                Tokenkey = token,
                                Status = true,
                                Date = DateTime.Now,
                                Exp = expirationTime,
                                PersonId = userRoles.PersonId
                            };
                            _context.Tokens.Add(tokenRecord);
                            _context.SaveChanges();
                        }
                        return user;
                    }
                }
                else
                {
                    return null; // Return null in case of issues with token claims
                }

            }

            return null;
        }

        [HttpPost("Customer")]
        [Authorize(Roles = "Customer")]
        public IActionResult SellersEndpoint()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var currentUser = GetCurrentUser(token);
            var user = _context.People.FirstOrDefault(o => o.PersonCode == currentUser.PersonCode);
            UsersData personResults = new UsersData();
            if (user != null)
            {
                var person = (from a in _context.People
                              join b in _context.Roles on a.RoleId equals b.Id
                              where a.PersonCode == user.PersonCode && a.Password == user.Password
                              select new UsersPerson
                              {
                                  PersonId = a.PersonId,
                                  PersonCode = a.PersonCode,
                                  Password = a.Password,
                                  PersonName = a.PersonName,
                                  RoleId = b.Id,
                                  Name = b.Name
                              }).FirstOrDefault();

                if (person == null)
                {
                    personResults.UsersPersons = null;

                }
                else
                {
                    personResults.UsersPersons = person;

                }
            }
            return Ok(personResults);
        }
    }
}
