using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanAPI.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}