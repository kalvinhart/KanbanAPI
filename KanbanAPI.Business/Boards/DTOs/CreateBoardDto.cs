using System.ComponentModel.DataAnnotations;

namespace KanbanAPI.Business.Boards.DTOs;

public record CreateBoardDto(
    [Required]
    [MaxLength(50)]
    string Name,
    [Required]
    List<CreateColumnDto> Columns);