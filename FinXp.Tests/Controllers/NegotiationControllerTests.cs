namespace FinXp.Tests.Controllers;

public class NegotiationControllerTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<INegotiationService> _negotiationService;

    private readonly NegotiationController _sut;

    public NegotiationControllerTests()
    {
        _mapper = new();
        _negotiationService = new();

        _sut = new NegotiationController(_negotiationService.Object, _mapper.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }
}
