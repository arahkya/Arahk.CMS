namespace Arahk.CMS.Application.Common;

public interface IUserIdProvider
{
    string GetUserName();
    Guid GetUserId();
    string GetUserEmailAddress();
}