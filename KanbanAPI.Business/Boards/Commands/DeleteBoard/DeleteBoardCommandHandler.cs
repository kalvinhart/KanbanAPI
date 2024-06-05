using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Business.Boards.Commands.DeleteBoard;

public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool?>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBoardCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool?> Handle(
        DeleteBoardCommand command,
        CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.BoardsRepository.GetByIdAsync(command.BoardId, new ContextGetParameters<Board>()
        {
            Includes = x => x
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
        });
        if (board is null) return null;

        ValidateBoardForDeletion(board);

        _unitOfWork.BoardsRepository.Remove(board);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static void ValidateBoardForDeletion(Board board)
    {
        if (board.Columns.Count == 0) return;
        if (board.Columns.Any(c => c.Cards.Count != 0))
            throw new InvalidOperationException("Board has cards and cannot be deleted");
    }
}