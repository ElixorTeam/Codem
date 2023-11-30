using System.Security.Claims;
using WebClient.Common;
using Сodem.Shared.Models;

namespace WebClient.Services;

public class UserService : IUserService
{
    private readonly ClaimsPrincipal? _user;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User ?? null;
    }

    private string UserAvatar => _user?.Claims.Where(c => c.Type.Equals("picture"))
        .Select(c => c.Value).FirstOrDefault() ?? string.Empty;

    private string GetUserClaim(string claim)
    {
        return _user?.FindFirst(claim)?.Value ?? string.Empty;
    }
    
    public UserModel? GetUser()
    {
        if (_user == null) return null;

        string id = GetUserClaim(ClaimTypes.NameIdentifier);
        string name = _user.Identity?.Name ?? string.Empty;

        if (string.IsNullOrEmpty(id)) return null;
        return new UserModel(id, name, UserAvatar);
    }
}