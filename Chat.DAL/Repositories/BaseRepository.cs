namespace Chat.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Chat.DAL.Infrastructure;
    using Chat.Domain.Entities;

    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        internal readonly DbContext context;

        private readonly DbSet<TEntity> dbSet;

        private readonly Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> defaultOrder = d => d.OrderByDescending(s => s.Id);

        protected BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;

            orderBy = orderBy ?? this.defaultOrder;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            foreach (var includeProperty in includeProperties.Split(
                new[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy(query).ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = this.dbSet.Find(id);
            this.Delete(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            this.dbSet.Remove(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.dbSet.Attach(entityToUpdate);
            this.context.SaveChanges();
        }

        #region Dispose pattern

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
                this.context?.Dispose();
            }
        }

        #endregion
    }
}