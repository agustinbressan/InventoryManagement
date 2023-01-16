using Application.Commands;
using Application.Interfaces;
using Moq;

namespace Test.Application.Commands;

public class CreateProductCommandTests
{
    [Fact]
    public async void CreateProductCommand_Valid_Arguments_Should_Call_ProductRepository_Create_Method()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new CreateProductCommandHandler(mockRepository.Object);

        // Act
        var result = await queryHandler.Handle(new CreateProductCommand("Product description", 1), default);

        // Assert
        mockRepository.Verify(x => x.CreateAsync("Product description", 1), Times.Once);
    }
}