using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gallery.Database;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Services
{
    /// <summary>
    /// Implements the operations specified in the <see cref="IDataService" />.
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// A database context instance representing a session with the database.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">A database context instance representing a session with the database.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dbContext" /> is
        /// <c>null</c>.</exception>
        public DataService(PostgresDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc />
        public async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));

            await _dbContext.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<TEntity>> GetEntitiesAsync<TEntity>() where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                                   .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetEntityAsync<TEntity>(long id) where TEntity : class
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        /// <inheritdoc />
        public async Task RemoveEntityAsync<TEntity>(long id) where TEntity : class
        {
            var entity = await GetEntityAsync<TEntity>(id);
            if (entity != null)
            {
                _dbContext.Remove(entity);

                await _dbContext.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));

            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
