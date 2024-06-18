using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Exceptions;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Columns.Mapping;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Business.Boards.Commands.UpdateBoard;

public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BoardDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBoardMapper _boardMapper;
    private readonly IColumnMapper _columnMapper;

    public UpdateBoardCommandHandler(
        IUnitOfWork unitOfWork,
        IBoardMapper boardMapper,
        IColumnMapper columnMapper)
    {
        _unitOfWork = unitOfWork;
        _boardMapper = boardMapper;
        _columnMapper = columnMapper;
    }

    public async Task<BoardDto?> Handle(UpdateBoardCommand command, CancellationToken cancellationToken)
    {
        ContextGetParameters<Board> parameters = new()
        {
            Includes = x => x.Include(y => y.Columns)
        };

        var board = await _unitOfWork.BoardsRepository.GetByIdAsync(command.BoardId, parameters);
        if (board is null) return null;

        board.Name = command.Name;

        UpdateColumnNames(board, command.Columns);

        await _unitOfWork.SaveChangesAsync();

        return _boardMapper.ToBoardDto(board);
    }

    private static void UpdateColumnNames(Board board, List<UpdateColumnDto> updatedColumns)
    {
        foreach (var updatedColumn in updatedColumns)
        {
            var existingColumn = board.Columns.FirstOrDefault(x => x.ColumnId == updatedColumn.ColumnId);
            if (existingColumn is null) throw new ColumnNotFoundException(updatedColumn.Name);

            existingColumn.Name = updatedColumn.Name;
        }
    }
}