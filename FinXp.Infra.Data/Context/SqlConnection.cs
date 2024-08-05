using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace FinXp.Infra.Data.Context;

public class SqlConnection(IConfiguration configuration) : ISqlConnection
{
    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
