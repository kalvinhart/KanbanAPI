using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using MediatR;

namespace KanbanAPI.Business.Boards.Commands.CreateBoard;

public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBoardMapper _mapper;

    public CreateBoardCommandHandler(
        IUnitOfWork unitOfWork,
        IBoardMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BoardDto> Handle(
        CreateBoardCommand request,
        CancellationToken cancellationToken)
    {
        if (await BoardExists(request.Name))
            throw new InvalidOperationException($"Board \"{request.Name}\" already exists");

        var board = _mapper.ToBoard(request);
        await _unitOfWork.BoardsRepository.AddAsync(board);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.ToBoardDto(board);
    }

    private async Task<bool> BoardExists(string name)
    {
        return await _unitOfWork.BoardsRepository.BoardExists(name);
    }
}