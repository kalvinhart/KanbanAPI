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
    [ProducesResponseType<List<BoardDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BoardDto>>> GetBoards()
    {
        var boards = await _boardsService.GetBoards();
        return Ok(boards);
    }

    [HttpPost]
    [ProducesResponseType<BoardDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardDto createBoardDto)
    {
        var board = await _boardsService.CreateBoard(createBoardDto);
        return CreatedAtAction(nameof(GetBoards), new { boardId = board.BoardId }, board);
    }

    [HttpPut]
    [ProducesResponseType<BoardDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> UpdateBoard(UpdateBoardDto updateBoardDto)
    {
        var board = await _boardsService.UpdateBoard(updateBoardDto);
        return Ok(board);
    }

    [HttpDelete("{boardId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteBoard(Guid boardId)
    {
        await _boardsService.DeleteBoard(boardId);
        return NoContent();
    }
}