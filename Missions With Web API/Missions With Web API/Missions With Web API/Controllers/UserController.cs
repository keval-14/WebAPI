using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MissionWebApi.Models;
using Project.Entities.Models.Mission.Mission_Request_Model;
using Project.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Missions_With_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _UserRepository;
        private readonly CIDBContext _context;
        private readonly IConfiguration _configuration;
        private string? statusCode;

        public UserController(CIDBContext context, IUserRepository userRepository, IConfiguration _IConfiguration)
        {
            _context = context;
            _UserRepository = userRepository;
            _configuration = _IConfiguration;
        }


        #region UserLogin
        // GET: api/MissionFilters
        [HttpPost("UserLogin")]
        public async Task<ActionResult<IEnumerable<Login>>> Login([FromBody] Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //string? StatusCode;
                var loginUser = _UserRepository.Login(login, out statusCode);
                if (loginUser != null)
                {

                    if (statusCode == HttpStatusCode.OK.ToString())
                    {

                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", loginUser.First().Email),
                        new Claim(ClaimTypes.Role, loginUser.First().Role)
                        //new Claim("LastLogin", user.LastLogin!)
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(500),
                            signingCredentials: signIn);

                        List<String> obj = new List<String>();
                        var loginToken = new JwtSecurityTokenHandler().WriteToken(token);
                        obj.Add(loginToken);
                        obj.Add("true");

                        return Ok(new { data = obj });


                    }
                    else
                    {
                        return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: (int)HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    List<String> obj = new List<String>();
                    obj.Add("Please Check Credentials...!");
                    obj.Add("false");
                    return Ok(new { data = obj });
                }

            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion


    }
}
