using Npgsql;

namespace FinXp.Infra.Data.Context;

public interface ISqlConnection
{
    NpgsqlConnection CreateConnection();
}
