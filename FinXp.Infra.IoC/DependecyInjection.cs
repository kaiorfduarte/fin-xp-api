using FinXp.Application.Services;
using FinXp.Domain.Interfaces.Repository;
using FinXp.Domain.Interfaces.Service;
using FinXp.Infra.Data.Context;
using FinXp.Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinXp.Infra.IoC;

public static class DependecyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddTransient<INegotiationService, NegotiationService>();
        service.AddTransient<INegotiationRepository, NegotiationRepository>();
        service.AddScoped<ISqlConnection, SqlConnection>();
        return service;
    }

    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("DefaultConnection") ??
        //    throw new ApplicationException("string connection is null");

        //service.AddNpgsqlDataSource(connectionString);

        return service;
    }
}
