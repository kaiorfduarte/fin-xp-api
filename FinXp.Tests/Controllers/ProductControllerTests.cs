using FinXp.Domain.Model;
using FinXp.Domain.Util;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FinXp.Domain.Model.Response;
using FluentAssertions;

namespace FinXp.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IProductService> _productService;

    private readonly ProductController _sut;

    public ProductControllerTests()
    {
        _mapper = new();
        _productService = new();

        _sut = new ProductController(_productService.Object, _mapper.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task GetProductList_WhenGetListProduct_ShouldReturnStatus200()
    {
        //Arrange
        ArrangeWhenGetListProductReturn200();

        //Act
        var actionResult = await _sut.GetProductList();
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetProductListAsync(), Times.Once);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetProductList_WhenGetListProductEmpty_ShouldReturnStatus204()
    {
        //Arrange
        var list = new List<Product>();

        var serviceResult = new ServiceResult<IEnumerable<Product>>().SetSuccess(list);

        _productService.Setup(x => x.GetProductListAsync())
            .ReturnsAsync(serviceResult);

        //Act
        var actionResult = await _sut.GetProductList();
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetProductListAsync(), Times.Once);
        _mapper.Verify(x => x.Map(It.IsAny<ProductResponse>(), It.IsAny<Product>()), Times.Never);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task GetProductList_WhenGetListProductException_ShouldReturnStatus500()
    {
        //Arrange
        var serviceResult = new ServiceResult<IEnumerable<Product>>().SetError(new Exception());

        _productService.Setup(x => x.GetProductListAsync())
            .ReturnsAsync(serviceResult);

        //Act
        var actionResult = await _sut.GetProductList();
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetProductListAsync(), Times.Once);
        _mapper.Verify(x => x.Map(It.IsAny<ProductResponse>(), It.IsAny<Product>()), Times.Never);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async Task GetClientProductList_WhenGetListClientProduct_ShouldReturnStatus200()
    {
        //Arrange
        const int clientId = 1;
        ArrangeWhenGetListClientProduct200();

        //Act
        var actionResult = await _sut.GetClientProductList(clientId);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetClientProductAsync(It.IsAny<int>()), Times.Once);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetClientProductList_WhenGetListClientProductEmpty_ShouldReturnStatus204()
    {
        //Arrange
        const int clientId = 1;
        var list = new List<ClientProduct>();

        var serviceResult = new ServiceResult<IEnumerable<ClientProduct>>().SetSuccess(list);

        _productService.Setup(x => x.GetClientProductAsync(clientId))
            .ReturnsAsync(serviceResult);

        //Act
        var actionResult = await _sut.GetClientProductList(clientId);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetClientProductAsync(It.IsAny<int>()), Times.Once);
        _mapper.Verify(x => x.Map(It.IsAny<ProductResponse>(), It.IsAny<Product>()), Times.Never);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Fact]
    public async Task GetClientProductList_WhenGetListClientProductException_ShouldReturnStatus500()
    {
        //Arrange
        const int clientId = 1;
        var serviceResult = new ServiceResult<IEnumerable<ClientProduct>>().SetError(new Exception());

        _productService.Setup(x => x.GetClientProductAsync(It.IsAny<int>()))
            .ReturnsAsync(serviceResult);

        //Act
        var actionResult = await _sut.GetClientProductList(clientId);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _productService.Verify(x => x.GetClientProductAsync(It.IsAny<int>()), Times.Once);
        _mapper.Verify(x => x.Map(It.IsAny<ClientProductResponse>(), It.IsAny<ClientProduct>()), Times.Never);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    private void ArrangeWhenGetListProductReturn200()
    {
        var productItem = new Product(1, "Produto A", 10, DateTime.Now, DateTime.Now);

        var productList = new List<Product>()
        {
            productItem
        };

        var productResponseList = new List<ProductResponse>()
        {
           new (productItem.ProductId, productItem.Name, productItem.Quantity, productItem.RegisterDate, productItem.ProductDueDate)
        };

        var serviceResult = new ServiceResult<IEnumerable<Product>>().SetSuccess(productList);

        _productService.Setup(x => x.GetProductListAsync())
            .ReturnsAsync(serviceResult);

        _mapper.Setup(x => x.Map<IEnumerable<ProductResponse>>(It.IsAny<IEnumerable<Product>>()))
            .Returns(productResponseList);
    }

    private void ArrangeWhenGetListClientProduct200()
    {
        const int clientId = 1;
        var clientProductItem = new ClientProduct(1, "Produto A", 10);

        var productList = new List<ClientProduct>()
        {
            clientProductItem
        };

        var clientProductResponseList = new List<ClientProductResponse>()
        {
           new (clientProductItem.ProductId, clientProductItem.Name, clientProductItem.Quantity)
        };

        var serviceResult = new ServiceResult<IEnumerable<ClientProduct>>().SetSuccess(productList);

        _productService.Setup(x => x.GetClientProductAsync(clientId))
            .ReturnsAsync(serviceResult);

        _mapper.Setup(x => x.Map<IEnumerable<ClientProductResponse>>(It.IsAny<IEnumerable<ClientProduct>>()))
            .Returns(clientProductResponseList);
    }
}
