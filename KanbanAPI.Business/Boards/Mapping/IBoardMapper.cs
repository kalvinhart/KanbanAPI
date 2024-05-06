using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.DataAccess.Boards.Entities;

namespace KanbanAPI.Business.Boards.Mapping;

public interface IBoardMapper
{
    BoardDto ToBoardDto(Board board);
    Board ToBoard(CreateBoardDto createBoardDto);
}