using Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Database
{
    /// <summary>
    /// A Postgres database context instance representing a session with the database.
    /// </summary>
    public class PostgresDbContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The options to be used by this database context.</param>
        /// <returns></returns>
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// A <see cref="DbSet{TEntity}" /> to be used to query and save instances of <see cref="Image" />.
        /// </summary>
        public virtual DbSet<Image> Images { get; set; }
    }
}
