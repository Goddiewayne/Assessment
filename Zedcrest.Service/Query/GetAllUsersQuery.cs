using MediatR;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Query
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
    }
}
