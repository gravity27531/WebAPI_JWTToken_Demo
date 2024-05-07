using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Viewmodels.Persons;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ReservationDevContext _context;

        public PersonsController(ReservationDevContext context)
        {
            _context = context;  
        }
        [HttpPost]
        
        public async Task<IActionResult> FPerson(PersonParam personParams)
        {
            PersonResult personResults = new PersonResult();
            try
            {
                var jwttoken = _context.Tokens.FirstOrDefault(t => t.Tokenkey ==  personParams.Tokenkey);
                if(jwttoken != null && jwttoken.Status == true)
                {
                    var person = (from a in _context.People
                                  join b in _context.Roles on a.RoleId equals b.Id
                                  where a.PersonId == personParams.PersonId
                                  select new Personrespon
                                  {
                                      PersonId = a.PersonId,
                                      PersonCode = a.PersonCode,
                                      Password = a.Password,
                                      PersonName = a.PersonName,
                                      RoleId = b.Id,
                                      RoleName = b.Name
                                  }).FirstOrDefault();

                    if (person == null)
                    {
                        personResults.Status = false;
                        personResults.Message = "ไม่พบข้อมูล";
                        personResults.Personrespons = null;

                    }
                    else
                    {
                        personResults.Status = true;
                        personResults.Message = "Found";
                        personResults.Personrespons = person;

                    }
                }
                else
                {
                    personResults.Status = false;
                    personResults.Message = "TokenKey หมดอายุแล้ว";
                    personResults.Personrespons = null;
                }
                
            }
            catch (Exception ex)
            {
                personResults.Status = false;
                personResults.Message = ex.Message;
                personResults.Personrespons = null;
                
            }
            return Ok(personResults);
        }

    }
}
