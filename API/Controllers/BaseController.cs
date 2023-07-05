using AutoMapper;
using Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private IMapper _mapper;

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

    protected Guid CurrentUserID => GetUserID();

    protected Guid GetUserID()
    {
        var userID = HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id");
        if (userID is null)
        {
            return Guid.Empty;
        }
        return new Guid(userID.Value);
    }

    public bool IsAdmin => IsInRole(PolicyName.ADMIN);
    public bool IsDoctor => IsInRole(PolicyName.DOCTOR);
    public bool IsStaff => IsInRole(PolicyName.STAFF);
    public bool IsCustomer => IsInRole(PolicyName.CUSTOMER);
    private bool IsInRole(string role) {
        return User.IsInRole(role);
    }
}
