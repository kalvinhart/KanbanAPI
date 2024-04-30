using KanbanAPI.DataAccess.Cards.Entities;

namespace KanbanAPI.DataAccess.Subtasks.Entities;

public class Subtask
{
    public Guid SubtaskId { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Guid CardId { get; set; }
    public required Card Card { get; set; }
}