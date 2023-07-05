using API.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Linq.Expressions;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMapper _mapper;

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected string CurrentUserID => GetUserID();

    protected string GetUserID()
    {
        var userID = HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id");
        if (userID is null)
        {
            return "";
        }
        return userID.Value;
    }

    public bool IsAdmin => IsCurrentUserAdmin();
    private bool IsCurrentUserAdmin() {
        return User.IsInRole(PolicyName.ADMIN);
    }
}
