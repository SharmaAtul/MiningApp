using Microsoft.EntityFrameworkCore;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : EntityBase
    {
        private DbSet<T> _entities;

        protected IDataContext Context { get; }

        public EntityFrameworkRepository(IDataContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(new[] { id }, cancellationToken);
        }

        public void InsertDeferred(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Insert(T entity)
        {
            InsertDeferred(entity);

            Context.SaveChanges();
        }

        public Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            InsertDeferred(entity);

            return Context.SaveChangesAsync(cancellationToken);
        }

        public void InsertDeferred(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            InsertDeferred(entities);

            Context.SaveChanges();
        }

        public Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            InsertDeferred(entities);

            return Context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateDeferred(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            UpdateDeferred(entity);

            Context.SaveChanges();
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            UpdateDeferred(entity);

            return Context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateDeferred(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            UpdateDeferred(entities);

            Context.SaveChanges();
        }

        public Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            UpdateDeferred(entities);

            return Context.SaveChangesAsync(cancellationToken);
        }

        public void DeleteDeferred(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            DeleteDeferred(entity);

            Context.SaveChanges();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            DeleteDeferred(entity);

            return Context.SaveChangesAsync(cancellationToken);
        }

        public void DeleteDeferred(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            DeleteDeferred(entities);

            Context.SaveChanges();
        }

        public Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            DeleteDeferred(entities);

            return Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table => ApplyDefaultIncludes(Entities);

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking => ApplyDefaultIncludes(Entities).AsNoTracking();

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                return _entities ?? (_entities = Context.EntitySet<T>());
            }
        }

        ///// <summary>
        ///// Gets a collection of changed property info. This method is only reliable with a tracked entity that has been loaded, modified, but not saved.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public ICollection<Tiger.DataModel.ChangedPropertyInfo> GetModifiedPropertyInfo(T entity)
        //{
        //    if (entity == null) throw new ArgumentNullException(nameof(entity));

        //    var changedProperties = new List<Tiger.DataModel.ChangedPropertyInfo>();

        //    var modifiedProperties = Context.ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Modified && e.Entity.Equals(entity))
        //        .SelectMany(e => e.Properties)
        //        .Where(p => p.IsModified)
        //        .Select(modifiedProperty => new Tiger.DataModel.ChangedPropertyInfo(
        //            modifiedProperty.Metadata.Name,
        //            modifiedProperty.Metadata.Relational().ColumnName, modifiedProperty.OriginalValue,
        //            modifiedProperty.CurrentValue)
        //            )
        //            .ToList();

        //    return modifiedProperties;
        //}

        public bool HasModifiedProperties(T entity) => Context.ChangeTracker.Entries<T>().Any(e => e.Entity.Equals(entity) && e.State == EntityState.Modified);

        protected virtual IQueryable<T> ApplyDefaultIncludes(IQueryable<T> source)
        {
            return source;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await Context.EntitySet<T>().ToListAsync();
        }

        public IQueryable<T> GetQureryableList(ISpecification<T> spec)
        {
            return ApplySpecification(spec);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Context.EntitySet<T>().AsQueryable(), spec);
        }
        //Task<IQueryable<T>> IRepository<T>.ListAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IQueryable<T>> IRepository<T>.ListAsync(ISpecification<T> spec)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
