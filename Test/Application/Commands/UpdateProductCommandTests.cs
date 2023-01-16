using Application.Commands;
using Application.Interfaces;
using Domain.Models;
using Moq;

namespace Test.Application.Commands;

public class UpdateProductCommandTests
{

    [Fact]
    public async void UpdateProductCommand_With_Not_Existing_Product_Id_Should_Throw_Exception()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException("Product not found mock message."));
        var queryHandler = new UpdateProductCommandHandler(mockRepository.Object);
        var mockUpdatedProduct = new Product(Guid.NewGuid(), "Updated description", 10);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await queryHandler.Handle(new UpdateProductCommand(mockUpdatedProduct.Id, mockUpdatedProduct.Description, mockUpdatedProduct.Stock), default));

        // Verify the exception message
        Assert.Equal("Product not found mock message.", exception.Message);
        mockRepository.Verify(x => x.UpdateAsync(mockUpdatedProduct.Id, mockUpdatedProduct.Description, mockUpdatedProduct.Stock), Times.Once);
    }

    [Fact]
    public async void UpdateProductCommand_Valid_Arguments_Should_Call_ProductRepository_Update_Method()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new UpdateProductCommandHandler(mockRepository.Object);
        var mockUpdatedProduct = new Product(Guid.NewGuid(), "Updated description", 10);

        // Act
        var result = await queryHandler.Handle(new UpdateProductCommand(mockUpdatedProduct.Id, mockUpdatedProduct.Description, mockUpdatedProduct.Stock), default);

        // Assert
        mockRepository.Verify(x => x.UpdateAsync(mockUpdatedProduct.Id, mockUpdatedProduct.Description, mockUpdatedProduct.Stock), Times.Once);
    }
}