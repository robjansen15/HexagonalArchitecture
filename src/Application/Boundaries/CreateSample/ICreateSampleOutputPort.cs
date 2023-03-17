namespace Application.Boundaries.CreateSample;

public interface ICreateSampleOutputPort
{
    void Success(CreateSampleOutput output);

    void Error(string message);
}