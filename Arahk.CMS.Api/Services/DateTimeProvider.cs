using Arahk.CMS.Application.Common;

namespace Arahk.CMS.Api.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime() => DateTime.Now;
}