using Zedcrest.Domain.Entities;

namespace Zedcrest.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAndUniqueRef(string email, string uniqueRef);
    }
}
