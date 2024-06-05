using KanbanAPI.Business.Boards.Mapping;

namespace KanbanAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IBoardMapper, BoardMapper>();

        return services;
    }
}