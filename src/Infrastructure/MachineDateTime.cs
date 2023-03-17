using Application.Interfaces.Infrastructure;

namespace Infrastructure;

public class MachineDateTime : IDateTime
{
    public DateTime UtcNow => DateTime.Now;
}