using FinXp.Domain.Interfaces.Repository;
using Dapper;
using FinXp.Infra.Data.Context;
using Npgsql;
using FinXp.Domain.Model;

namespace FinXp.Infra.Data.Repository;

public class NegotiationRepository(ISqlConnection _sqlConnection) : INegotiationRepository
{
    public async Task<bool> SaveBuyNegotiationDataAsync(NegotiationProduct negotiationProduction)
    {
        await using NpgsqlConnection connection = _sqlConnection.CreateConnection();
        connection.Open();
        await using var tran = connection.BeginTransaction();

        var result = await connection.ExecuteScalarAsync<bool>(
            "SELECT SaveBuyNegotiation(@ClientId, @ProductId, @Quantity);",
            negotiationProduction);

        tran.Commit();
        return result;
    }

    public async Task<bool> SaveSellNegotiationDataAsync(NegotiationProduct negotiationProduction)
    {
        await using NpgsqlConnection connection = _sqlConnection.CreateConnection();
        connection.Open();
        await using var tran = connection.BeginTransaction();

        var result = await connection.ExecuteScalarAsync<bool>(
            "SELECT SaveSellNegotiation(@ClientId, @ProductId, @Quantity);",
            negotiationProduction);

        tran.Commit();
        return result;
    }
}
