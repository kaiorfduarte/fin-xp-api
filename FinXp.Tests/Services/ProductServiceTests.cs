using FinXp.Domain.Model;
using FluentAssertions;

namespace FinXp.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<ILogger<ProductService>> _logger;

    private readonly ProductService _sut;

    public ProductServiceTests()
    {
        _productRepository = new();
        _logger = new();

        _sut = new(_productRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetClientProductAsync_WhenClientIdIsValid_ShouldSuccessResult()
    {
        //Arrange
        const int clientId = 1;
        var dataResult = CreateMockList(new ClientProduct(1, "Product A", 10));

        _productRepository.Setup(x => x.GetClientProductDataAsync(It.IsAny<int>()))
            .ReturnsAsync(dataResult);

        //Act
        var result = await _sut.GetClientProductAsync(clientId);

        //Assert
        _productRepository.Verify(x => x.GetClientProductDataAsync(It.IsAny<int>()), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetClientProductAsync_WhenClientHasNotProduct_ShouldSuccessResult()
    {
        //Arrange
        const int clientId = 1;
        var dataResult = CreateMockList(default(ClientProduct));

        _productRepository.Setup(x => x.GetClientProductDataAsync(It.IsAny<int>()))
            .ReturnsAsync(dataResult);

        //Act
        var result = await _sut.GetClientProductAsync(clientId);

        //Assert
        _productRepository.Verify(x => x.GetClientProductDataAsync(It.IsAny<int>()), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetClientProductAsync_WhenErrorException_ShouldErrorResult()
    {
        //Arrange
        const int clientId = 1;

        _productRepository.Setup(x => x.GetClientProductDataAsync(It.IsAny<int>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _sut.GetClientProductAsync(clientId);

        //Assert
        _productRepository.Verify(x => x.GetClientProductDataAsync(It.IsAny<int>()), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetProductListAsync_WhenHasProduct_ShouldSuccessResult()
    {
        //Arrange
        var dataResult = CreateMockList(new Product(1, "Product A", 10, DateTime.Now, DateTime.Now));

        _productRepository.Setup(x => x.GetProductListDataAsync())
            .ReturnsAsync(dataResult);

        //Act
        var result = await _sut.GetProductListAsync();

        //Assert
        _productRepository.Verify(x => x.GetProductListDataAsync(), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductListAsync_WhenHasNotProduct_ShouldSuccessResult()
    {
        //Arrange
        var dataResult = CreateMockList(default(Product));

        _productRepository.Setup(x => x.GetProductListDataAsync())
            .ReturnsAsync(dataResult);

        //Act
        var result = await _sut.GetProductListAsync();

        //Assert
        _productRepository.Verify(x => x.GetProductListDataAsync(), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductAsync_WhenErrorException_ShouldErrorResult()
    {
        //Arrange
        _productRepository.Setup(x => x.GetProductListDataAsync())
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _sut.GetProductListAsync();

        //Assert
        _productRepository.Verify(x => x.GetProductListDataAsync(), Times.Once);
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    private static List<T> CreateMockList<T>(T? item)
    {
        var list = new List<T>();

        if (item is not null)
        {
            list.Add(item);
        }

        return list;
    }
}
