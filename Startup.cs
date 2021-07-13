using System;
using Gallery.Database;
using Gallery.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gallery
{
    /// <summary>
    /// The startup class for the web host.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// A set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">A set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Configures the services specified in the <paramref name="serviceCollection" />.
        /// </summary>
        /// <param name="serviceCollection">A collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString(nameof(PostgresDbContext))))
                             .AddTransient<IDataService, DataService>()
                             .AddTransient<IImageService, ImageService>()
                             .AddControllers();
        }

        /// <summary>
        /// Configures the application's request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            _ = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));

            applicationBuilder.UseHttpsRedirection()
                              .UseRouting()
                              .UseDefaultFiles()
                              .UseStaticFiles()
                              .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
