namespace Exam.Data.Repositories
{
    using System.Linq;

    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity Find(object id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        TEntity Delete(TEntity entity);

        TEntity Delete(object id);

        int SaveChanges();
    }
}
