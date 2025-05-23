using Microsoft.EntityFrameworkCore;
using PatientRegistrations.Domain.Helpers;
using PatientRegistrations.Domain.Interfaces;

namespace PatientRegistration.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TEntity GetByID(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public void DeleteByID(int id)
        {
            _context.Set<TEntity>().Where(x => x.Id == id).ExecuteDelete();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public void Save(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
    }
}
