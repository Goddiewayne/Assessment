using MediatR;
using Zedcrest.Data.Repository;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.AddAsync(request.User);
        }
    }
}
