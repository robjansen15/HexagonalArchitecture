using FunctionalTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace FunctionalTests;

public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                ServiceDescriptor descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));

                // Build the service provider.
                ServiceProvider serviceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context (CleanArchitectureDbContext).
                using IServiceScope scope = serviceProvider.CreateScope();

                IServiceProvider scopedServices = scope.ServiceProvider;
                AppDbContext db = scopedServices.GetRequiredService<AppDbContext>();
                ILogger<CustomWebApplicationFactory<TStartup>> logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    DatabaseInitializer.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }