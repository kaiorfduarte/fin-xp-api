using FinXp.Domain.Model;
using FluentAssertions;

namespace FinXp.Tests.Services;

public class NegotiationServiceTests
{
    private readonly Mock<INegotiationRepository> _negotiationRepository;
    private readonly Mock<ILogger<NegotiationService>> _logger;

    private readonly NegotiationService _sut;

    private static NegotiationProduct BuyEntryParam;
    private static NegotiationProduct SellEntryParam;

    public NegotiationServiceTests()
    {
        _negotiationRepository = new();
        _logger = new();

        _sut = new(_negotiationRepository.Object, _logger.Object);

        BuyEntryParam = new NegotiationProduct(1, 1, 10, Domain.Enum.OperationType.Buy);
        SellEntryParam = new NegotiationProduct(1, 1, 10, Domain.Enum.OperationType.Sell);
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenNegotiationIsBuyer_SholdSuccessResult()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(true);

        //Act
        var result = await _sut.SaveNegotiationAsync(BuyEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeTrue();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenNegotiationIsSeller_SholdSuccessResult()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(true);

        //Act
        var result = await _sut.SaveNegotiationAsync(SellEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeTrue();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenHasNotProductToBuy_SholdSuccessResultAndDataFalse()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(false);

        //Act
        var result = await _sut.SaveNegotiationAsync(BuyEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeFalse();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenHasNotProductToSell_SholdSuccessResultAndDataFalse()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ReturnsAsync(false);

        //Act
        var result = await _sut.SaveNegotiationAsync(SellEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().BeFalse();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenBuyErrorException_SholdErrorResult()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _sut.SaveNegotiationAsync(BuyEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeFalse();
    }

    [Fact]
    public async Task SaveNegotiationAsync_WhenSellErrorException_SholdErrorResult()
    {
        //Arrange
        _negotiationRepository.Setup(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()))
            .ThrowsAsync(It.IsAny<Exception>());

        //Act
        var result = await _sut.SaveNegotiationAsync(SellEntryParam);

        //Assert
        _negotiationRepository.Verify(x => x.SaveBuyNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Never);
        _negotiationRepository.Verify(x => x.SaveSellNegotiationDataAsync(It.IsAny<NegotiationProduct>()), Times.Once);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Data.Should().BeFalse();
    }
}
