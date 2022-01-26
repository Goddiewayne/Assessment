using Zedcrest.Domain.Entities;

namespace Zedcrest.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly ApplicationDbContext ApplicationDbContext;

        public GenericRepository(ApplicationDbContext customDbContext) => ApplicationDbContext = customDbContext;

        public IEnumerable<TEntity> GetAll() => ApplicationDbContext.Set<TEntity>();

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await ApplicationDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await ApplicationDbContext.AddAsync(entity);
            await ApplicationDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            ApplicationDbContext.Update(entity);
            await ApplicationDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            var itemToRemove = await ApplicationDbContext.Set<TEntity>().FindAsync(entity.Id);
            ApplicationDbContext.Remove(itemToRemove);
            await ApplicationDbContext.SaveChangesAsync();
            return itemToRemove;
        }
    }
}
