using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.DataAccess.Boards.Entities;
using Riok.Mapperly.Abstractions;

namespace KanbanAPI.Business.Boards.Mapping;

[Mapper]
public partial class BoardMapper : IBoardMapper
{
    public partial BoardDto ToBoardDto(Board board);
    public partial Board ToBoard(CreateBoardDto createBoardDto);
}