using Application.Commands;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using Moq;

namespace Test.Application.Commands;

public class UpdateProductCommandTests
{

    [Fact]
    public async void UpdateProductCommand_With_Non_Existing_Product_Should_Throw_ProductNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new UpdateProductCommandHandler(mockRepository.Object);
        var mockProductId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ProductNotFoundException>(async () => await queryHandler.Handle(new UpdateProductCommand(mockProductId, "Mock description", 10), default));

        // Verify the exception message
        Assert.Equal($"Not found a Product with Id: {mockProductId}", exception.Message);
        mockRepository.Verify(x => x.GetByIdAsync(mockProductId), Times.Once);
        mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async void UpdateProductCommand_Valid_Arguments_Should_Update_The_Product_And_Save_Changes()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new UpdateProductCommandHandler(mockRepository.Object);

        var mockProductId = Guid.NewGuid();
        var existingProduct = new Product(mockProductId, "Mock product", 2);
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingProduct);

        // Act
        var result = await queryHandler.Handle(new UpdateProductCommand(mockProductId, "Mock new description", 1), default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Mock new description", result.Description);
        Assert.Equal(1, result.Stock);
        mockRepository.Verify(x => x.GetByIdAsync(mockProductId), Times.Once);
        mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}