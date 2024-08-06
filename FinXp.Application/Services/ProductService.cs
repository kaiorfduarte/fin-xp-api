using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Interfaces.Service;
using FinXp.Domain.Model;
using Microsoft.Extensions.Logging;

namespace FinXp.Application.Services;

public class ProductService(IProductRepository productRepository,
    ILogger<ProductService> logger) : IProductService
{
    public async Task<ServiceResult<IList<ClientProduct>>> GetClientProductAsync(int clientId)
    {
        try
        {
            var clientProductList = await productRepository.GetClientProductDataAsync(clientId);

            return clientProductList.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("Erro ao obter lista do {ClientId} - {Message}", clientId, ex.Message);

            return ex;
        }
    }

    public async Task<ServiceResult<IList<Product>>> GetProductListAsync()
    {
        try
        {
            var productList = await productRepository.GetProductListDataAsync();

            return productList.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError("Erro ao obter lista de produtos - {Message}", ex.Message);

            return ex;
        }
    }
}
