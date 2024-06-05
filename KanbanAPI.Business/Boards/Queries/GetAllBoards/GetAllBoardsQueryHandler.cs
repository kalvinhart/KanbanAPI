using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Business.Boards.Queries.GetAllBoards;

public class GetAllBoardsQueryHandler : IRequestHandler<GetAllBoardsQuery, List<BoardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBoardMapper _mapper;

    public GetAllBoardsQueryHandler(
        IUnitOfWork unitOfWork,
        IBoardMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BoardDto>> Handle(
        GetAllBoardsQuery request,
        CancellationToken cancellationToken)
    {
        var boards = await _unitOfWork.BoardsRepository.GetAllAsync(new ContextGetParameters<Board>()
        {
            Includes = x => x
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .ThenInclude(c => c.Subtasks),
            DisableTracking = true
        });

        return boards
            .Select(_mapper.ToBoardDto)
            .ToList();
    }
}