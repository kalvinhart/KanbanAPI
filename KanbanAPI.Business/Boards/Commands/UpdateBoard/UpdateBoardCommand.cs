using KanbanAPI.Business.Boards.DTOs;
using MediatR;

namespace KanbanAPI.Business.Boards.Commands.UpdateBoard;

public record UpdateBoardCommand(Guid BoardId, string Name) : IRequest<BoardDto?>;