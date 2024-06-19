using KanbanAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        KanbanDbContext dbContext = scope.ServiceProvider.GetRequiredService<KanbanDbContext>();

        dbContext.Database.Migrate();
    }
}