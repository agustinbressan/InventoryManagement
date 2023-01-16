using Application.Interfaces;
using Application.Queries;
using Domain.Models;
using Moq;

namespace Test.Application.Queries;

public class GetAllProductsQueryTests
{

    [Fact]
    public async void GetAllProductsQuery_Should_Return_Existing_Products()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new [] {
            new Product(Guid.NewGuid(), "Mock Product 1", 1),
            new Product(Guid.NewGuid(), "Mock Product 2", 2),
        });
        var queryHandler = new GetAllProductsQuery.Handler(mockRepository.Object);

        // Act
        var queryResult = await queryHandler.Handle(new GetAllProductsQuery(), default);

        // Assert
        Assert.NotNull(queryResult);
        Assert.Equal(2, queryResult.Count());
        mockRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }
}