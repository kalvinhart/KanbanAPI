namespace KanbanAPI.Business.Subtasks.DTOs;

public record SubtaskDto(
    Guid SubtaskId,
    string Title,
    bool IsCompleted,
    Guid CardId);