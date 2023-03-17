using System.Net;
using System.Text;
using Api;
using Api.Features.Sample.CreateSample;
using FluentAssertions;
using FunctionalTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence;

namespace FunctionalTests.Tests;

public class SampleTests: IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
{
    private readonly CustomWebApplicationFactory<Startup> factory;
    
    public SampleTests(CustomWebApplicationFactory<Startup> factory) => this.factory = factory;
    
    [Fact]
    public async Task It_Creates_Sample_And_Verifies_The_Id_Is_Not_0()
    {
        // Arrange
        HttpClient client = factory.CreateClient();
        var createPollRequest = new CreateSampleRequest()
        {
            Name = "name3"
        };
        var httpContent = new StringContent(JsonConvert.SerializeObject(createPollRequest), Encoding.UTF8, "application/json");

        // Act
        HttpResponseMessage response = await client.PostAsync("/api/Sample", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        string responseString = await response.Content.ReadAsStringAsync();
        int actualPollId = JsonConvert.DeserializeObject<int>(responseString);
        actualPollId.Should().NotBe(0);
    }
    

    public void Dispose()
    {
        using IServiceScope scope = this.factory.Server.Services.CreateScope();

        IServiceProvider scopedServices = scope.ServiceProvider;
        AppDbContext dbContext = scopedServices.GetRequiredService<AppDbContext>();
        DatabaseInitializer.ReinitializeDbForTests(dbContext);
    }
}