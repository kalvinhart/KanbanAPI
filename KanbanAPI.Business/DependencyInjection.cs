using Microsoft.Extensions.DependencyInjection;

namespace KanbanAPI.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}