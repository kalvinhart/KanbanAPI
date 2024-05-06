using KanbanAPI.Business.Cards.DTOs;

namespace KanbanAPI.Business.Columns.DTOs;

public record ColumnDto(
    Guid ColumnId,
    string Name,
    int Index,
    Guid BoardId,
    List<CardDto> Cards);