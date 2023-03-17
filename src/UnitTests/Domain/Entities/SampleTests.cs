using Domain.Entities;
using Domain.SeedWork;
using FluentAssertions;

namespace UnitTests.Domain.Entities;

public class SampleTests
{
    [Fact]
    public void Initialize_WithValidData_ShouldCreateNewOption()
    {
        // Arrange
        const string name = "fake name";

        // Act
        var sample = new Sample(name);

        // Assert
        sample.Name.Should().Be(name);
    } 
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void It_Throws_Exception_When_Invalid(string text)
    {
        // Arrange
        const string expectedExceptionMessage = "Name cannot be null.";

        // Act
        DomainException actualException = Assert.Throws<DomainException>(() => new Sample(text));

        // Assert
        actualException.Message.Should().Contain(expectedExceptionMessage);
    }

}