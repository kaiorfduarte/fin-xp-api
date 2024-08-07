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
}
