using API.Models.Request;
using API.Models.Response;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers;

[Route("/v1/bcs/auth")]
public class AuthController : BaseController
{
    private readonly IRepositoryBase<User> _userRepo;

    public AuthController(IRepositoryBase<User> userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest credentials)
    {
        var user = await _userRepo.FirstOrDefaultAsync(
            u => u.Email.Equals(credentials.Email) && u.Password.Equals(credentials.Password)
            );
        LoginResponse loginResponse = new();
        if (user == null)
        {
            throw new UnauthorizedException();
        }
        else
        {
            loginResponse.AccessToken = GenerateToken(user);
            SetCookie(Constants.ACCESS_TOKEN, loginResponse.AccessToken);
        }
        return Ok(loginResponse);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        RemoveCookie(Constants.ACCESS_TOKEN);
        return Ok();
    }

    private string GenerateToken(User user)
    {
        var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        return new JwtSecurityTokenHandler().WriteToken(
            GenerateTokenByClaims(claims, DateTime.Now.AddMinutes(120))
            );
    }

    private SecurityToken GenerateTokenByClaims(Claim[] claims, DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    private void RemoveCookie(string key)
    {
        HttpContext.Response.Cookies.Delete(key);
    }

    private void SetCookie(string key, string value)
    {
        CookieOptions cookieOptions = new();
        cookieOptions.HttpOnly = true;
        cookieOptions.Expires = DateTime.Now.AddDays(2);
        HttpContext.Response.Cookies.Append(key, value);
    }
}
