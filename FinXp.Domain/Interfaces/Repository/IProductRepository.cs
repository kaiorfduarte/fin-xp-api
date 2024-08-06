using FinXp.Domain.Model;

namespace FinXp.Domain.Interfaces.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductListDataAsync();

    Task<IEnumerable<ClientProduct>> GetClientProductDataAsync(int clientId);
}
