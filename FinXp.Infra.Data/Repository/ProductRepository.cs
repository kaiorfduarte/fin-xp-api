using Dapper;
using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Model;
using FinXp.Infra.Data.Context;
using Npgsql;

namespace FinXp.Infra.Data.Repository;

public class ProductRepository(ISqlConnection _sqlConnection) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetProductListDataAsync()
    {
        await using NpgsqlConnection connection = _sqlConnection.CreateConnection();

        return await connection.QueryAsync<Product>(
            "SELECT ProductId, Name, Quantity, RegisterDate, ProductDueDate " +
            "FROM Product;");
    }

    public async Task<IEnumerable<ClientProduct>> GetClientProductDataAsync(int clientId)
    {
        await using NpgsqlConnection connection = _sqlConnection.CreateConnection();

        return await connection.QueryAsync<ClientProduct>(
            "SELECT P.ProductId, P.Name, CP.Quantity " +
            "FROM ClientProduct CP " +
            "INNER JOIN Product P ON P.ProductId = CP.ProductId " +
            $"WHERE CP.ClientId = {clientId};");
    }
}
