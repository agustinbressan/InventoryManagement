using Application.Commands;
using Application.Interfaces;
using Domain.Models;
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
        var mockProductDesciption = "Mock description";
        var mockProductStock = 50;
        mockRepository.Setup(x => x.CreateAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Product(Guid.NewGuid(), mockProductDesciption, mockProductStock));
        
        // Act
        var result = await queryHandler.Handle(new CreateProductCommand(mockProductDesciption, mockProductStock), default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockProductDesciption, result.Description);
        Assert.Equal(mockProductStock, result.Stock);
        mockRepository.Verify(x => x.CreateAsync(mockProductDesciption, mockProductStock), Times.Once);
    }
}