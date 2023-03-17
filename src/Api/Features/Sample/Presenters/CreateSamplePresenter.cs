using Application.Boundaries.CreateSample;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Sample.Presenters;

public class CreateSamplePresenter : ICreateSampleOutputPort
{
    public IActionResult ViewModel { get; private set; }

    public void Error(string message)
    {
        this.ViewModel = new BadRequestObjectResult(new
        {
            ErrorMessage = message
        });
    }
    
    public void Success(CreateSampleOutput output) => this.ViewModel = new CreatedResult(string.Empty, output.Id);
}