using API.Models.Request.Auth;
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
    private readonly IRepositoryBase<User> _usrRepo;
    private readonly AppSettings _appSettings;

    public AuthController(IRepositoryBase<User> usrRepo, IOptions<AppSettings> appSettings)
    {
        _usrRepo = usrRepo;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest credentials)
    {
        var user = await _usrRepo.FirstOrDefaultAsync(u => u.Username.Equals(credentials.Username));
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
        if (!registerRequest.Password.Equals(registerRequest.ReEnteredPassword))
        {
            throw new BadRequestException("Re-entered password incorrect");
        }
        var userSdb = await _usrRepo.FirstOrDefaultAsync(u => u.Username == registerRequest.Username);
        if (userSdb != null)
        {
            throw new BadRequestException("User already exist");
        }
        var passwordHasher = new PasswordHasher<User>();
        var user = Mapper.Map(registerRequest, new User());
        user.Password = passwordHasher.HashPassword(user, user.Password);
        await _usrRepo.CreateAsync(user);
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
                new Claim(ClaimTypes.NameIdentifier, user.Username),
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
        CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(2),
            IsEssential = true,
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        };
        Response.Cookies.Append(key, value, cookieOptions);
    }
}
