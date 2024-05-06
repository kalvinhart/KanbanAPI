using KanbanAPI.Business.Boards.Mapping;

namespace KanbanAPI.Extensions.Services;

public static class Mappers
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IBoardMapper, BoardMapper>();
        return services;
    }
}