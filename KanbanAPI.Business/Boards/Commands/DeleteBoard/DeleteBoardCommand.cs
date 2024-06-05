using MediatR;

namespace KanbanAPI.Business.Boards.Commands.DeleteBoard;

public record DeleteBoardCommand(Guid BoardId) : IRequest<bool?>;