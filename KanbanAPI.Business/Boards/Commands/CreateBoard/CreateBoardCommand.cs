using KanbanAPI.Business.Boards.DTOs;
using MediatR;

namespace KanbanAPI.Business.Boards.Commands.CreateBoard;

public record CreateBoardCommand(string Name, List<CreateColumnDto> Columns) : IRequest<BoardDto>;