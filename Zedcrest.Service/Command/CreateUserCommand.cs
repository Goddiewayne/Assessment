using MediatR;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Command
{
    public class CreateUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }
}
