using Arahk.CMS.Application.Common;

namespace Arahk.CMS.Api.Services;

public class UserIdProvider : IUserIdProvider
{
    public Guid GetUserId()
    {
        return Guid.Parse("{ee13a032-042f-442c-b015-1be6780f9a76}");
    }
}