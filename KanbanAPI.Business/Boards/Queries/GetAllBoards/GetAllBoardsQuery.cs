using KanbanAPI.Business.Boards.DTOs;
using MediatR;

namespace KanbanAPI.Business.Boards.Queries.GetAllBoards;

public record GetAllBoardsQuery() : IRequest<List<BoardDto>>;