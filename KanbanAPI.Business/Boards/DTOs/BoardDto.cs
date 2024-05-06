using KanbanAPI.Business.Columns.DTOs;

namespace KanbanAPI.Business.Boards.DTOs;

public record BoardDto(
    Guid BoardId,
    string Name,
    List<ColumnDto> Columns);