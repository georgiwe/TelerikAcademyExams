namespace Exam.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    public class BullsAndCowsEFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private IDbSet<TEntity> dbSet;

        public BullsAndCowsEFRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public TEntity Find(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Update(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public TEntity Delete(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
            return entity;
        }

        public TEntity Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);

            return entity;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = state;
        }
    }
}
