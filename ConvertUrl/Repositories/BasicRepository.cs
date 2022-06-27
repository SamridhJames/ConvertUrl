using ConvertUrlRepository.Domain;
using ConvertUrlRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConvertUrlRepository.Repositories
{
    public class BasicRepository<TEntity> : IBasicRepository<TEntity> where TEntity : BaseUrlEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> Db;

        public BasicRepository(ApplicationDbContext context)
        {
            _context = context;
            Db = context.Set<TEntity>();
        }

        public TEntity Insert(TEntity entity)
        {
            if (entity != null)
            {
                Db.Add(entity);
            }
            return entity;
        }

        public bool SaveChange()
        {
            _context.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
