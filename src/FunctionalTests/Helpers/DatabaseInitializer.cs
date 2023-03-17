using Persistence;

namespace FunctionalTests.Helpers;

internal static class DatabaseInitializer
{
    internal static void InitializeDbForTests(AppDbContext dbContext)
    {
        PredefinedData.InitializeSamples();
        
        dbContext.Sample.AddRange(PredefinedData.Samples);

        dbContext.SaveChanges();
    }

    internal static void ReinitializeDbForTests(AppDbContext dbContext)
    {
        dbContext.Sample.RemoveRange(dbContext.Sample);
        
        InitializeDbForTests(dbContext);
    }
}