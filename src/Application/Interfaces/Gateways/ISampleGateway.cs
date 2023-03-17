using Domain.Entities;

namespace Application.Interfaces.Gateways;

public interface ISampleGateway
{
    Task<Sample?> GetAsync(int id);
    Task CreateAsync(Sample sample);
    Task UpdateAsync(Sample sample);
    Task DeleteAsync(Sample sample);
}