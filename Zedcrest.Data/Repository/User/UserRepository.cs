using Microsoft.EntityFrameworkCore;
using Zedcrest.Domain.Entities;

namespace Zedcrest.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _icontext;
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _icontext = applicationDbContext;
        }
        public async Task<User> GetUserByEmailAndUniqueRef(string email, string uniqueRef)
        {
            var query = _icontext.Users.Where(x => x.EmailAddress == email && x.UniqueReference == uniqueRef);
            var users = await query.ToListAsync();
            var user = users.FirstOrDefault();
            if (user != null)
            {
                var uploads = _icontext.FileUploads.Where(x => x.UserId == user.Id).ToList();
                user.FileUploads = uploads;
            }
            return user;
        }
    }
}
