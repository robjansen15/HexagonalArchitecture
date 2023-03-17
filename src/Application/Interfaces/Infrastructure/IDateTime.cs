namespace Application.Interfaces.Infrastructure;

public interface IDateTime
{
    DateTime UtcNow { get; }
}