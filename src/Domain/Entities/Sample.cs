using Domain.SeedWork;

namespace Domain.Entities;

public class Sample : Entity
{
    public Sample(string name) : base()
    {
        this.SetName(name);
    }
    
    public string Name { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException($"Name cannot be null.");

        this.Name = name;
    }
}