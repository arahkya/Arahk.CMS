namespace Arahk.CMS.Application.Common;

public interface IUserIdProvider
{
    Guid GetUserId();
}