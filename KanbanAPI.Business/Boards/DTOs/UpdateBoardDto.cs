using System.ComponentModel.DataAnnotations;

namespace KanbanAPI.Business.Boards.DTOs;

public record UpdateBoardDto(
    [Required] Guid BoardId,
    [MaxLength(50)] string Name,
    List<UpdateColumnDto> Columns);