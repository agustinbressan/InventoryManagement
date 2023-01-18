using Application.Commands;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using Moq;

namespace Test.Application.Commands;

public class DeleteProductCommandTests
{
    [Fact]
    public async void DeleteProductCommand_With_Non_Existing_Product_Should_Throw_ProductNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new DeleteProductCommandHandler(mockRepository.Object);
        var mockDeleteProductId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ProductNotFoundException>(async () => await queryHandler.Handle(new DeleteProductCommand(mockDeleteProductId), default));

        // Verify the exception message
        Assert.Equal($"Not found a Product with Id: {mockDeleteProductId}", exception.Message);
        mockRepository.Verify(x => x.GetByIdAsync(mockDeleteProductId), Times.Once);
        mockRepository.Verify(x => x.Delete(It.IsAny<Product>()), Times.Never);
        mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async void DeleteProductCommand_Valid_Arguments_Should_Call_ProductRepository_Delete_Method()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new DeleteProductCommandHandler(mockRepository.Object);
        var mockDeleteProductId = Guid.NewGuid();
        var existingProduct = new Product(mockDeleteProductId, "Mock product", 1);
        mockRepository.Setup(x => x.GetByIdAsync(mockDeleteProductId)).ReturnsAsync(existingProduct);

        // Act
        var result = await queryHandler.Handle(new DeleteProductCommand(mockDeleteProductId), default);

        // Assert
        mockRepository.Verify(x => x.GetByIdAsync(mockDeleteProductId), Times.Once);
        mockRepository.Verify(x => x.Delete(existingProduct), Times.Once);
        mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
}