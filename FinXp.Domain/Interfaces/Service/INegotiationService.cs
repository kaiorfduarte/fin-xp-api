using FinXp.Domain.Model;
using FinXp.Domain.Util;

namespace FinXp.Domain.Interfaces.Service;

public interface INegotiationService
{
    Task<ServiceResult<bool>> SaveNegotiationAsync(NegotiationProduct negotiationProduction);
}
