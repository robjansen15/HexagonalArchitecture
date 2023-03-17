using Api.Features.Sample.Presenters;
using Application.Boundaries.CreateSample;
using Application.Interfaces.Gateways;
using Application.Interfaces.Infrastructure;
using Application.UseCases;
using Infrastructure;
using Infrastructure.Logger;
using Persistence.Repositories;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        // Infrastructure:
        services.AddTransient<IDateTime, MachineDateTime>();
        services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

        // Gateways:
        services.AddScoped<ISampleGateway, SampleGateway>();

        // Use cases:
        services.AddScoped<ICreateSampleInputPort, CreateSampleUseCase>();

        // Presenters:
        services.AddScoped<CreateSamplePresenter>();

        return services;
    }
}