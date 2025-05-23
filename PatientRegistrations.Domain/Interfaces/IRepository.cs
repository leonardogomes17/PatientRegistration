
namespace PatientRegistrations.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity GetByID(int id);

        IQueryable<TEntity> GetAll();

        void Save(TEntity entity);

        void DeleteByID(int id);

    }
}
