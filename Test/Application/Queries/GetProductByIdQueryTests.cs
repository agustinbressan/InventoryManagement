using Application.Interfaces;
using Application.Queries;
using Domain.Models;
using Moq;

namespace Test.Application.Queries;

public class GetProductByIdQueryTests
{

    [Fact]
    public async void GetProductByIdQuery_Should_Return_The_Correct_Product()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockExistingProduct = new Product(Guid.NewGuid(), "Mock Product 1", 1);
        mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockExistingProduct);
        var queryHandler = new GetProductByIdQueryHandler(mockRepository.Object);

        // Act
        var queryResult = await queryHandler.Handle(new GetProductByIdQuery(mockExistingProduct.Id), default);

        // Assert
        Assert.NotNull(queryResult);
        Assert.Equal(queryResult.Description, mockExistingProduct.Description);
        Assert.Equal(queryResult.Stock, mockExistingProduct.Stock);
        mockRepository.Verify(x => x.GetByIdAsync(mockExistingProduct.Id), Times.Once);
    }
}