using KanbanAPI.Business.Boards.Commands.UpdateBoard;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Moq;

namespace KanbanAPI.Business.Tests.Boards.Commands;

[TestFixture]
public class UpdateBoardCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdateBoardCommandHandler _handler;

    public UpdateBoardCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new UpdateBoardCommandHandler(_unitOfWork.Object, new BoardMapper());
    }

    [SetUp]
    public void SetUp()
    {
        _unitOfWork.Reset();
    }

    [Test]
    public async Task UpdateBoard_ShouldReturnBoardDto_WhenSuccessful()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var command = new UpdateBoardCommand(boardId, "Board 1");

        var board = new Board
        {
            BoardId = boardId,
            Name = "Old Board"
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(It.IsAny<Guid>()).Result)
            .Returns(board);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result!.Name, Is.EqualTo("Board 1"));
    }

    [Test]
    public async Task UpdateBoard_ShouldReturnNull_WhenBoardNotFound()
    {
        // Arrange
        var command = new UpdateBoardCommand(Guid.NewGuid(), "Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(It.IsAny<Guid>()).Result)
            .Returns<Board>(null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}