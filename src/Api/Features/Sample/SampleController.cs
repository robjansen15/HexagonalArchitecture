using Api.Features.Sample.CreateSample;
using Api.Features.Sample.Presenters;
using Application.Boundaries.CreateSample;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Sample;

[Route("api/[controller]")]
[ApiController]
public class SampleController : ControllerBase
{
    private readonly ICreateSampleInputPort _createSampleInputPort;
    private readonly CreateSamplePresenter _createSamplePresenter;

    public SampleController(
        ICreateSampleInputPort createSampleInputPort,
        CreateSamplePresenter createSamplePresenter)
    {
        _createSampleInputPort = createSampleInputPort;
        _createSamplePresenter = createSamplePresenter;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePoll(CreateSampleRequest request)
    {
        var createPollInput = new CreateSampleInput(request.Name);
        await _createSampleInputPort.HandleAsync(createPollInput, _createSamplePresenter);
        return _createSamplePresenter.ViewModel;
    }
}