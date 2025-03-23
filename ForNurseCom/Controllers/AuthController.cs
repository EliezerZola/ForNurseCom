using ForNurseCom.ModelsMaria;
using ForNurseCom.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ForNurseCom.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _JwtSettings;

        KmedicDbContext dbC = new KmedicDbContext();

        public AuthController(JwtSettings jwtSettings)
        {
            _JwtSettings = jwtSettings;
        }


        
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] KeyRequest request)
        {
            if (request.Key != "aadfed856a929973df011ed961e02fc75e49a266cd6fbafe5a9ab6f43a85c82f" && request.Key != "f78a59908cbb0d3766152761b62f6f21d927260366da32037a1352ab85fb7d95")
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Invalid Key");
            }
            //else if (request.Key != "f78a59908cbb0d3766152761b62f6f21d927260366da32037a1352ab85fb7d95")
            //{
            //    return StatusCode((int)HttpStatusCode.Forbidden, "Invalid Key");
            //}
            else
            {
                var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Eliezer Zola"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                var token = new JwtSecurityToken(
                    _JwtSettings.Issuer,
                    _JwtSettings.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(6),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }




            
        }
    }
}
