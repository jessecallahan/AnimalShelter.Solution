using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnimalShelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TokenController : ControllerBase
  {
    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateToken([FromBody] LoginModel login)
    {
      IActionResult response = Unauthorized();
      var user = Authenticate(login);
      if (user != null)
      {
        var tokenString = BuildToken(user);
        response = Ok(new { token = tokenString, tokenExpire = DateTime.Now.AddMinutes(30) });
      }
      return response;
    }
    private UserModel Authenticate(LoginModel login)
    {
      UserModel user = null;
      if (login.Username == "User1" && login.Password == "Rockstar")
      {
        user = new UserModel { Name = "User", Email = "user@gmail.com" };
      }
      return user;
    }
    private string BuildToken(UserModel user)
    {
      var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.SecretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(issuer: Startup.Issuer, audience: Startup.Audience, claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}