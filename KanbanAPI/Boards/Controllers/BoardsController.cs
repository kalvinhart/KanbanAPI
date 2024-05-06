using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Boards.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardsController : ControllerBase
{
    private readonly IBoardsService _boardsService;

    public BoardsController(IBoardsService boardsService)
    {
        _boardsService = boardsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BoardDto>>> GetBoards()
    {
        var boards = await _boardsService.GetBoards();
        return Ok(boards);
    }

    [HttpPost]
    public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardDto createBoardDto)
    {
        var board = await _boardsService.CreateBoard(createBoardDto);
        return CreatedAtAction(nameof(GetBoards), new { boardId = board.BoardId }, board);
    }

    [HttpPut]
    public async Task<ActionResult<BoardDto>> UpdateBoard(UpdateBoardDto updateBoardDto)
    {
        var board = await _boardsService.UpdateBoard(updateBoardDto);
        return Ok(board);
    }

    [HttpDelete("{boardId}")]
    public async Task<ActionResult> DeleteBoard(Guid boardId)
    {
        await _boardsService.DeleteBoard(boardId);
        return NoContent();
    }
}