using Application.Boundaries.CreateSample;
using Application.Interfaces.Gateways;
using Application.UseCases;
using FakeItEasy;

namespace UnitTests.Application.UseCases;

public class CreateSampleUseCaseTests
{
    [Fact]
    public async Task HandleAsync_WithValidPoll_ShouldCallSuccess()
    {
        // Arrange
        ISampleGateway pollGatewayStub = A.Fake<ISampleGateway>();

        ICreateSampleOutputPort OutputPortMock = A.Fake<ICreateSampleOutputPort>();

        var useCase = new CreateSampleUseCase(null, pollGatewayStub);
        CreateSampleInput input = GetValidCreatePollInput();

        // Act
        await useCase.HandleAsync(input, OutputPortMock);

        // Assert
        A.CallTo(() => OutputPortMock.Success(
                A<CreateSampleOutput>.That.Matches(
                    p => p.Id == 0)))
            .MustHaveHappenedOnceExactly();
    }
    
    private static CreateSampleInput GetValidCreatePollInput()
    {
        return new CreateSampleInput("name");
    }
}