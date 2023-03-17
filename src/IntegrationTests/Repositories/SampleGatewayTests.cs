using Domain.Entities;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace IntegrationTests.Repositories;

public class SampleGatewayTests
{
    [Fact]
    public void It_Adds_Sample_Entity_To_Database()
    {
        // In-memory database only exists while the connection is open
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        try
        {
            DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema in the database
            using (var context = new AppDbContext(options))
                context.Database.EnsureCreated();
            
            string name = "test name";
            Sample poll = this.GetDefaultSample(name);

            // Run the test against one instance of the context
            using (var context = new AppDbContext(options))
            {
                var pollGateway = new SampleGateway(context);
                pollGateway.CreateAsync(poll);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new AppDbContext(options))
            {
                context.Sample.Count().Should().Be(1);
                context.Sample.Single().Name.Should().Be(name);
            }
        }
        finally
        {
            connection.Close();
        }
    }

    private Sample GetDefaultSample(string title)
    {
        return new Sample(title);
    }
}