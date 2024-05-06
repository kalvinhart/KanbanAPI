using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Business.Boards.Services;

public class BoardsService : IBoardsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBoardMapper _mapper;

    public BoardsService(
        IUnitOfWork unitOfWork,
        IBoardMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<BoardDto>> GetBoards()
    {
        var boards = await _unitOfWork.BoardsRepository.GetAllAsync();

        return boards
            .Select(_mapper.ToBoardDto)
            .ToList();
    }

    public async Task<BoardDto> CreateBoard(CreateBoardDto createBoardDto)
    {
        if (await BoardExists(createBoardDto.Name))
            throw new InvalidOperationException($"Board \"{createBoardDto.Name}\" already exists");

        var board = _mapper.ToBoard(createBoardDto);
        await _unitOfWork.BoardsRepository.AddAsync(board);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.ToBoardDto(board);
    }

    private async Task<bool> BoardExists(string name)
    {
        return await _unitOfWork.BoardsRepository.BoardExists(name);
    }

    public async Task<BoardDto> UpdateBoard(UpdateBoardDto updateBoardDto)
    {
        var board = await _unitOfWork.BoardsRepository.GetByIdAsync(updateBoardDto.BoardId);
        if (board is null) throw new InvalidOperationException("Board not found");

        board.Name = updateBoardDto.Name;
        await _unitOfWork.SaveChangesAsync();

        return _mapper.ToBoardDto(board);
    }

    public async Task DeleteBoard(Guid boardId)
    {
        var board = await _unitOfWork.BoardsRepository.GetByIdAsync(boardId, new ContextGetParameters<Board>()
        {
            Includes = x => x
                .Include(b => b.Columns)
                    .ThenInclude(c => c.Cards)
        });
        if (board is null) throw new InvalidOperationException("Board not found");

        ValidateBoardForDeletion(board);

        _unitOfWork.BoardsRepository.Remove(board);
        await _unitOfWork.SaveChangesAsync();
    }

    private static void ValidateBoardForDeletion(Board board)
    {
        if (board.Columns.Count == 0) return;
        if (board.Columns.Any(c => c.Cards.Count != 0))
            throw new InvalidOperationException("Board has cards and cannot be deleted");
    }
}