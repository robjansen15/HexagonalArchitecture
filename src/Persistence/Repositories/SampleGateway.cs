using Application.Interfaces.Gateways;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SampleGateway : ISampleGateway
{
    private readonly AppDbContext _dbContext;

    public SampleGateway(AppDbContext dbContext) => this._dbContext = dbContext;
    
    public Task<Sample> GetAsync(int id)
    {
        return _dbContext
            .Sample
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task CreateAsync(Sample sample)
    {
        _dbContext.Sample.Add(sample);
        return _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Sample sample)
    {
        _dbContext.Entry(sample).State = EntityState.Modified;
        return _dbContext.SaveChangesAsync();
    }
 
    public async Task DeleteAsync(Sample sample)
    {
        _dbContext.Entry(sample).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
    }
}