using KanbanAPI.Business.Subtasks.DTOs;

namespace KanbanAPI.Business.Cards.DTOs;

public record CardDto(
    Guid CardId,
    string Title,
    string Description,
    int Index,
    Guid ColumnId,
    List<SubtaskDto> Subtasks);