namespace Application.Boundaries.CreateSample;

public interface ICreateSampleInputPort
{
    Task HandleAsync(CreateSampleInput input, ICreateSampleOutputPort output);
}