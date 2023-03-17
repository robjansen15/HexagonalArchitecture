using Application.Boundaries.CreateSample;
using Application.Interfaces.Gateways;
using Application.Interfaces.Infrastructure;
using Domain.Entities;
using Domain.SeedWork;

namespace Application.UseCases;

public class CreateSampleUseCase : ICreateSampleInputPort
{
    private readonly ILoggerService<CreateSampleUseCase> _logger;
    private readonly ISampleGateway _sampleGateway;
    
    public CreateSampleUseCase(
        ILoggerService<CreateSampleUseCase> logger,
        ISampleGateway sampleGateway)
    {
        _logger = logger;
        _sampleGateway = sampleGateway;
    }
    
    public async Task HandleAsync(CreateSampleInput input, ICreateSampleOutputPort output)
    {
        try
        {
            var sample = new Sample(input.Name);

            await _sampleGateway.CreateAsync(sample);

            output.Success(new CreateSampleOutput(sample.Id));
        }
        catch (DomainException ex)
        {
            _logger.LogInformation("{@exception} occured when trying to create a sample with {@input}", ex, input);
        }
    }
}