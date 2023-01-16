using Application.Commands;
using Application.Interfaces;
using Moq;

namespace Test.Application.Commands;

public class DeleteProductCommandTests
{
    [Fact]
    public async void DeleteProductCommand_With_Not_Existing_Product_Id_Should_Throw_Exception()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(new KeyNotFoundException("Product not found mock message."));
        var queryHandler = new DeleteProductCommandHandler(mockRepository.Object);
        var mockDeleteProductId = Guid.NewGuid();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await queryHandler.Handle(new DeleteProductCommand(mockDeleteProductId), default));

        // Verify the exception message
        Assert.Equal("Product not found mock message.", exception.Message);
        mockRepository.Verify(x => x.DeleteAsync(mockDeleteProductId), Times.Once);
    }

    [Fact]
    public async void DeleteProductCommand_Valid_Arguments_Should_Call_ProductRepository_Delete_Method()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var queryHandler = new DeleteProductCommandHandler(mockRepository.Object);
        var mockDeleteProductId = Guid.NewGuid();

        // Act
        var result = await queryHandler.Handle(new DeleteProductCommand(mockDeleteProductId), default);

        // Assert
        mockRepository.Verify(x => x.DeleteAsync(mockDeleteProductId), Times.Once);
    }
}