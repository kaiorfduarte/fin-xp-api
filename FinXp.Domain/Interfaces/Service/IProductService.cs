using FinXp.Domain.Model;
using FinXp.Domain.Util;

namespace FinXp.Domain.Interfaces.Service;

public interface IProductService
{
    Task<ServiceResult<IEnumerable<Product>>> GetProductListAsync();

    Task<ServiceResult<IEnumerable<ClientProduct>>> GetClientProductAsync(int clientId);
}
