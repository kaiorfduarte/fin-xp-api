using FinXp.Domain.Model;
using FinXp.Domain.Model.Request;
using FinXp.Domain.Util;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FinXp.Tests.Controllers;

public class NegotiationControllerTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<INegotiationService> _negotiationService;

    private readonly NegotiationController _sut;

    private static NegotiationRequest NegotiationRequest;
    private static NegotiationProduct NegotiationProductModel;

    public NegotiationControllerTests()
    {
        _mapper = new();
        _negotiationService = new();

        _sut = new NegotiationController(_negotiationService.Object, _mapper.Object);

        Setup();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenCreateNewNegotiation_SholdReturnStatus201()
    {
        //Arrange
        var serviceResult = new ServiceResult<bool>().SetSuccess(true);

        _negotiationService.Setup(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(serviceResult);

        _mapper.Setup(x => x.Map<NegotiationProduct>(It.IsAny<NegotiationRequest>()))
            .Returns(NegotiationProductModel);

        //Act
        var actionResult = await _sut.SaveNegotiationAsync(NegotiationRequest);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _negotiationService.Verify(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenCreateNewNegotiation_SholdReturnStatus422()
    {
        //Arrange
        var serviceResult = new ServiceResult<bool>().SetSuccess(false);

        _negotiationService.Setup(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(serviceResult);

        _mapper.Setup(x => x.Map<NegotiationProduct>(It.IsAny<NegotiationRequest>()))
            .Returns(NegotiationProductModel);

        //Act
        var actionResult = await _sut.SaveNegotiationAsync(NegotiationRequest);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _negotiationService.Verify(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenCreateNewNegotiation_SholdReturnStatus500()
    {
        //Arrange
        var serviceResult = new ServiceResult<bool>().SetError(new Exception());

        _negotiationService.Setup(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(serviceResult);

        _mapper.Setup(x => x.Map<NegotiationProduct>(It.IsAny<NegotiationRequest>()))
            .Returns(NegotiationProductModel);

        //Act
        var actionResult = await _sut.SaveNegotiationAsync(NegotiationRequest);
        var statusCodeResult = (IStatusCodeActionResult)actionResult;

        //Assert
        _negotiationService.Verify(x => x.SaveNegotiationAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    private static void Setup()
    {
        NegotiationRequest = new NegotiationRequest()
        {
            ClientId = 1,
            OperationTypeId = Domain.Enum.OperationType.Buy,
            ProductId = 1,
            Quantity = 10
        };

        NegotiationProductModel = new NegotiationProduct(NegotiationRequest.ProductId,
            NegotiationRequest.ClientId,
            NegotiationRequest.Quantity,
            NegotiationRequest.OperationTypeId);
    }
}
