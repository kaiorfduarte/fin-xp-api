using FinXp.Domain.Enum;
using FinXp.Domain.Exceptions;
using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model;
using FinXp.Domain.Util;
using Microsoft.Extensions.Logging;

namespace FinXp.Application.Services;

public class NegotiationService(INegotiationRepository negotiationRepository,
    ILogger<NegotiationService> logger) : INegotiationService
{
    public async Task<ServiceResult<bool>> SaveNegotiationAsync(NegotiationProduct negotiationProduction)
    {
        var result = new ServiceResult<bool>();

        try
		{
            if(negotiationProduction.OperationTypeId == OperationType.Buy)
            {
                result.SetSuccess(await negotiationRepository.SaveBuyNegotiationDataAsync(negotiationProduction));
            }
            else
            {
                result.SetSuccess(await negotiationRepository.SaveSellNegotiationDataAsync(negotiationProduction));
            }

            return result;
        }
		catch (Exception ex)
		{
            logger.LogError("Ocorre um erro para salvar a negociacao {Message}", ex.Message);

            return result.SetError(ex.Message);
        }
    }
}
