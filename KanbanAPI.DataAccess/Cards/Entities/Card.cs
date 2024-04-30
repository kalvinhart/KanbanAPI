using KanbanAPI.DataAccess.Columns.Entities;
using KanbanAPI.DataAccess.Subtasks.Entities;

namespace KanbanAPI.DataAccess.Cards.Entities;

public class Card
{
    public Guid CardId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Index { get; set; }

    public Guid ColumnId { get; set; }
    public required Column Column { get; set; }
    public ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
}