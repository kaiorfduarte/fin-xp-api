using FinXp.Domain.Enum;
using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model;
using Microsoft.Extensions.Logging;

namespace FinXp.Application.Services;

public class NegotiationService(INegotiationRepository negotiationRepository, ILogger<NegotiationService> logger) : INegotiationService
{
    public async Task<ServiceResult<bool>> SaveNegotiationAsync(NegotiationProduct negotiationProduction)
    {
        try
		{
            if(negotiationProduction.OperationTypeId == OperationType.Buy)
            {
                return await negotiationRepository.SaveBuyNegotiationDataAsync(negotiationProduction);
            }
            else
            {
                return await negotiationRepository.SaveSellNegotiationDataAsync(negotiationProduction);
            }
        }
		catch (Exception ex)
		{
            logger.LogError("Ocorre um erro para salvar a negociacao {Message}", ex.Message);
            return ex;
        }
    }
}
