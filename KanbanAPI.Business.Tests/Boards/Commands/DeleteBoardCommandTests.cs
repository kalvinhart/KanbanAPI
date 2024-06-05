using KanbanAPI.Business.Boards.Commands.DeleteBoard;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Cards.Entities;
using KanbanAPI.DataAccess.Columns.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Moq;

namespace KanbanAPI.Business.Tests.Boards.Commands;

[TestFixture]
public class DeleteBoardCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DeleteBoardCommandHandler _handler;

    public DeleteBoardCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new DeleteBoardCommandHandler(_unitOfWork.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _unitOfWork.Reset();
    }

    [Test]
    public async Task DeleteBoard_ShouldDeleteBoard_WhenSuccessful()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var command = new DeleteBoardCommand(boardId);
        var board = new Board
        {
            BoardId = boardId,
            Name = "Board 1",
            Columns = new List<Column>()
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns(board);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWork.Verify(x => x.BoardsRepository.Remove(board), Times.Once);
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteBoard_ShouldReturnNull_WhenBoardNotFound()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var command = new DeleteBoardCommand(boardId);

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns<Board>(null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void DeleteBoard_ShouldThrowInvalidOperationException_WhenBoardHasCards()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var command = new DeleteBoardCommand(boardId);
        var columnId = Guid.NewGuid();
        var card1 = new Card
        {
            CardId = Guid.NewGuid(),
            Title = "Card 1",
            Index = 0,
            ColumnId = columnId
        };
        var column1 = new Column()
        {
            ColumnId = columnId,
            Name = "Column 1",
            Cards = new List<Card>()
            {
                card1
            }
        };

        var board = new Board
        {
            BoardId = boardId,
            Name = "Board 1",
            Columns = new List<Column>
            {
                column1
            }
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns(board);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}