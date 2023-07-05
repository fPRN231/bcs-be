using API.Models.Request;
using API.Models.Response;
using Domain.Application.AppConfig;
using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[Route("/v1/bcs/auth")]
public class AuthController : BaseController
{
    private readonly IRepositoryBase<User> _userRepo;
    private readonly AppSettings _appSettings;

    public AuthController(IRepositoryBase<User> userRepo, IOptions<AppSettings> appSettings)
    {
        _userRepo = userRepo;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest credentials)
    {
        var user = await _userRepo.FirstOrDefaultAsync(u => u.Email.Equals(credentials.Email));
        LoginResponse loginResponse = new();
        if (user == null)
        {
            throw new UnauthorizedException();
        }
        else
        {
            var passwordHasher = new PasswordHasher<User>();
            var isMatchPassword = passwordHasher.VerifyHashedPassword(user, user.Password, credentials.Password) == PasswordVerificationResult.Success;
            if (!isMatchPassword)
            {
                throw new UnauthorizedException();
            }
            loginResponse.AccessToken = GenerateToken(user);
            SetCookie(Constants.ACCESS_TOKEN, loginResponse.AccessToken);
        }
        return Ok(loginResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var userSdb = await _userRepo.FirstOrDefaultAsync(u => u.Email == registerRequest.Email);
        if (userSdb != null)
        {
            throw new BadRequestException("User already exist");
        }
        var passwordHasher = new PasswordHasher<User>();
        var user = Mapper.Map(registerRequest, new User());
        user.Password = passwordHasher.HashPassword(user, user.Password);
        await _userRepo.CreateAsync(user);
        var response = Mapper.Map(user, new RegisterResponse());
        return Ok(response);
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

    private SecurityToken GenerateTokenByClaims(Claim[] claims, DateTime expires)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWTOptions.IssuerSigningKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        return new JwtSecurityToken(_appSettings.JWTOptions.ValidIssuer,
             _appSettings.JWTOptions.ValidAudience,
             claims,
             expires: expires,
             signingCredentials: credentials);
    }

    private void RemoveCookie(string key)
    {
        HttpContext.Response.Cookies.Delete(key);
    }

    private void SetCookie(string key, string value)
    {
        CookieOptions cookieOptions = new();
        cookieOptions.HttpOnly = true;
        cookieOptions.SameSite = SameSiteMode.None;
        cookieOptions.Expires = DateTime.Now.AddDays(2);
        HttpContext.Response.Cookies.Append(key, value, cookieOptions);
    }
}
