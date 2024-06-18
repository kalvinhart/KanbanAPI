using KanbanAPI.Business.Boards.Commands.CreateBoard;
using KanbanAPI.Business.Boards.Commands.DeleteBoard;
using KanbanAPI.Business.Boards.Commands.UpdateBoard;
using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Boards.Queries.GetAllBoards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Boards.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IBoardMapper _mapper;

    public BoardsController(
        ISender sender,
        IBoardMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<BoardDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BoardDto>>> GetBoards()
    {
        var boards = await _sender.Send(new GetAllBoardsQuery());
        return Ok(boards);
    }

    [HttpPost]
    [ProducesResponseType<BoardDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardDto createBoardDto)
    {
        var command = new CreateBoardCommand(createBoardDto.Name, createBoardDto.Columns);
        var board = await _sender.Send(command);

        return CreatedAtAction(
            nameof(GetBoards),
            new { boardId = board.BoardId },
            board);
    }

    [HttpPut]
    [ProducesResponseType<BoardDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BoardDto>> UpdateBoard(UpdateBoardDto updateBoardDto)
    {
        var command = new UpdateBoardCommand(
            updateBoardDto.BoardId,
            updateBoardDto.Name,
            updateBoardDto.Columns);
        var board = await _sender.Send(command);

        return board is null ? NotFound() : Ok(board);
    }

    [HttpDelete("{boardId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBoard(Guid boardId)
    {
        var command = new DeleteBoardCommand(boardId);
        var result = await _sender.Send(command);

        return result is null ? NotFound() : NoContent();
    }
}