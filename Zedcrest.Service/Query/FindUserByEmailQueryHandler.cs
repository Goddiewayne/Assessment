using MediatR;
using Zedcrest.Data.Repository;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Service.Query
{
    public class FindUserByEmailQueryHandler : IRequestHandler<FindUserByEmailQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public FindUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByEmailAndUniqueRef(request.EmailAddress, request.UniqueReference);
        }
    }
}
