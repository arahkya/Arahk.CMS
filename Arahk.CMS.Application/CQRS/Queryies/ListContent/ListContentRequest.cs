using Arahk.CMS.Domain.CMS;
using MediatR;

namespace Arahk.CMS.Application.CQRS.Queryies.ListContent;

public class ListContentRequest : IRequest<IEnumerable<Content>>
{

}