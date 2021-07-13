using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.Services
{
    /// <summary>
    /// An interface that specifies the operations a data service should implement.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Asynchronously adds the specified <paramref name="entity" /> to the backing data store.
        /// <para>
        /// This method calls <c>SaveChangesAsync()</c>.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity" /> is <c>null</c>.</exception>
        Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Asynchronously gets all entities with the specified type <typeparamref name="TEntity" /> from the backing
        /// data store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task wrapping a list of all entities with the specified type <typeparamref name="TEntity"
        /// />.</returns>
        Task<List<TEntity>> GetEntitiesAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Asynchronously gets the entity with the specified <paramref name="id" /> from the backing data store if it
        /// exists.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task wrapping the entity with the specified <paramref name="id" />.</returns>
        Task<TEntity> GetEntityAsync<TEntity>(long id) where TEntity : class;

        /// <summary>
        /// Asynchronously removes the entity with the specified <paramref name="id" /> from the backing data store if
        /// it exists.
        /// <para>
        /// This method calls <c>SaveChangesAsync()</c> if the entity to be removed exists.
        /// </para>
        /// </summary>
        /// <param name="id">The ID of the entity to remove.</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        Task RemoveEntityAsync<TEntity>(long id) where TEntity : class;

        /// <summary>
        /// Asynchronously updates the specified <paramref name="entity" /> in the backing data store.
        /// <para>
        /// This method calls <c>SaveChangesAsync()</c>.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A task wrapping this asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity" /> is <c>null</c>.</exception>
        Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
