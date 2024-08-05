using FinXp.Domain.Model;

namespace FinXp.Domain.Interfaces.Repository;

public interface INegotiationRepository
{
    Task<bool> SaveBuyNegotiationDataAsync(NegotiationProduct negotiationProduction);

    Task<bool> SaveSellNegotiationDataAsync(NegotiationProduct negotiationProduction);
}
