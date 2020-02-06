using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiningVentilation.Core
{
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Entity</returns>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Insert entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Adds <paramref name="entity"/> to change tracker, but does not persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        void InsertDeferred(T entity);

        /// <summary>
        /// Insert entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task InsertAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Insert entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Adds <paramref name="entities"/> to change tracker, but does not persist them.
        /// </summary>
        /// <param name="entities">Entity</param>
        void InsertDeferred(IEnumerable<T> entities);

        /// <summary>
        /// Insert entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Update entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Updates <paramref name="entity"/> in change tracker, but does not persist changes.
        /// </summary>
        /// <param name="entity">Entity</param>
        void UpdateDeferred(T entity);

        /// <summary>
        /// Update entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Updates <paramref name="entities"/> in change tracker, but does not persist changes.
        /// </summary>
        /// <param name="entities">Entity</param>
        void UpdateDeferred(IEnumerable<T> entities);

        /// <summary>
        /// Update entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Delete entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Marks <paramref name="entity"/> deleted in change tracker, but does not persist changes.
        /// </summary>
        /// <param name="entity">Entity</param>
        void DeleteDeferred(T entity);

        /// <summary>
        /// Delete entity and persist it.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Delete entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Marks <paramref name="entities"/> deleted in change tracker, but does not persist changes.
        /// </summary>
        /// <param name="entities">Entity</param>
        void DeleteDeferred(IEnumerable<T> entities);

        /// <summary>
        /// Delete entities and persist them.
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Queryable that queries can be built with.
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        /// <summary>
        /// Indicates if the given <paramref name="entity"/> has any changed properties.
        /// </summary>
        /// <param name="entity">The entity to inspect.</param>
        /// <returns>True if the <paramref name="entity"/> has changed; Otherwise false.</returns>
        bool HasModifiedProperties(T entity);

        IQueryable<T> GetQureryableList(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
