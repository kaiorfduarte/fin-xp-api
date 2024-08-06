using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model;
using FinXp.Domain.Util;
using Microsoft.Extensions.Logging;

namespace FinXp.Application.Services;

public class ProductService(IProductRepository productRepository,
    ILogger<ProductService> logger) : IProductService
{
    public async Task<ServiceResult<IEnumerable<ClientProduct>>> GetClientProductAsync(int clientId)
    {
        var result = new ServiceResult<IEnumerable<ClientProduct>>();
        try
        {
            var clientProductList = await productRepository.GetClientProductDataAsync(clientId);

            return result.SetSuccess(clientProductList);
        }
        catch (Exception ex)
        {
            logger.LogError("Erro ao obter lista do {ClientId} - {Message}", clientId, ex.Message);

            return result.SetError(ex.Message);
        }
    }

    public async Task<ServiceResult<IEnumerable<Product>>> GetProductListAsync()
    {
        var result = new ServiceResult<IEnumerable<Product>>();
        try
        {
            var productList = await productRepository.GetProductListDataAsync();

            return result.SetSuccess(productList);
        }
        catch (Exception ex)
        {
            logger.LogError("Erro ao obter lista de produtos - {Message}", ex.Message);

            return result.SetError(ex.Message);
        }
    }
}
