using Arahk.CMS.Application.Common;
using Arahk.CMS.Api.Authentication;

namespace Arahk.CMS.Api.Services;

public class UserIdProvider : IUserIdProvider
{
    private const string UnavailableString = "NA";
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string GetUserName()
    {
        string userName = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(p => p.Type.ToLower() == ClaimsNames.Name)?.Value ?? UnavailableString;

        return userName;
    }

    public Guid GetUserId()
    {        
        string userId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(p => p.Type.ToLower() == ClaimsNames.UserId)?.Value ?? UnavailableString;

        return Guid.Parse(userId);
    }

    public string GetUserEmailAddress()
    {        
        string emailAddress = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(p => p.Type.ToLower().Contains(ClaimsNames.EmailAddress))?.Value ?? UnavailableString;

        return emailAddress;
    }
}