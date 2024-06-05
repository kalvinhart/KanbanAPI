using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using MediatR;

namespace KanbanAPI.Business.Boards.Commands.UpdateBoard;

public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BoardDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBoardMapper _mapper;

    public UpdateBoardCommandHandler(IUnitOfWork unitOfWork,
        IBoardMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BoardDto?> Handle(
        UpdateBoardCommand command,
        CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.BoardsRepository.GetByIdAsync(command.BoardId);
        if (board is null) return null;

        board.Name = command.Name;
        await _unitOfWork.SaveChangesAsync();

        return _mapper.ToBoardDto(board);
    }
}