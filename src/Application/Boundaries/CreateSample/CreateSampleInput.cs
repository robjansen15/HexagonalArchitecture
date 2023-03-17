namespace Application.Boundaries.CreateSample;

public class CreateSampleInput
{
    public CreateSampleInput(string name)
    {
        this.Name = name;
    }
    
    public string Name { get; set; }
}