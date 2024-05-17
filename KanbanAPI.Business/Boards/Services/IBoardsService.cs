using KanbanAPI.Business.Boards.DTOs;

namespace KanbanAPI.Business.Boards.Services;

public interface IBoardsService
{
    Task<List<BoardDto>> GetBoards();
    Task<BoardDto> CreateBoard(CreateBoardDto createBoardDto);
    Task<BoardDto?> UpdateBoard(UpdateBoardDto updateBoardDto);
    Task<bool?> DeleteBoard(Guid boardId);
}