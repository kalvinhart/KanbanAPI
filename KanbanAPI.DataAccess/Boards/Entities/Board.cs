using KanbanAPI.DataAccess.Columns.Entities;

namespace KanbanAPI.DataAccess.Boards.Entities;

public class Board
{
    public Guid BoardId { get; set; }
    public required string Name { get; set; }

    public ICollection<Column> Columns { get; set; } = new List<Column>();
}