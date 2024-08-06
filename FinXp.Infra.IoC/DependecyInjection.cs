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
        service.AddTransient<IProductService, ProductService>();
        service.AddTransient<IProductRepository, ProductRepository>();
        service.AddScoped<ISqlConnection, SqlConnection>();
        return service;
    }
}
