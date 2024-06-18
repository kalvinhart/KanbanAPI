namespace KanbanAPI.Business.Boards.DTOs;

public record UpdateColumnDto(
    Guid ColumnId,
    string Name);