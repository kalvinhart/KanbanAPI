using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Columns.Mapping;

namespace KanbanAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IBoardMapper, BoardMapper>();
        services.AddScoped<IColumnMapper, ColumnMapper>();

        return services;
    }
}