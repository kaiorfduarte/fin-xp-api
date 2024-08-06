using FinXp.Domain.Model;

namespace FinXp.Domain.Interfaces.Service;

public interface IProductService
{
    Task<ServiceResult<IList<Product>>> GetProductListAsync();

    Task<ServiceResult<IList<ClientProduct>>> GetClientProductAsync(int clientId);
}
