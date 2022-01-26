using MediatR;
using Zedcrest.Data.Repository;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Query
{
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public FindUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByIdAsync(request.UserId);
        }
    }
}
