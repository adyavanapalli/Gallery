using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gallery
{
    /// <summary>
    /// Contains the <see cref="Main" /> method.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configures, builds, and runs the web host.
        /// </summary>
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }
}
