using FinXp.Domain.Model;

namespace FinXp.Domain.Interfaces.Service;

public interface INegotiationService
{
    Task<ServiceResult<bool>> SaveNegotiationAsync(NegotiationProduct negotiationProduction);
}
