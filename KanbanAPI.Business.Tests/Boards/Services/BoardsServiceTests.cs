using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Boards.Services;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Cards.Entities;
using KanbanAPI.DataAccess.Columns.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Moq;

namespace KanbanAPI.Business.Tests.Boards.Services;

[TestFixture]
public class BoardsServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly BoardsService _sut;

    public BoardsServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _sut = new BoardsService(_unitOfWork.Object, new BoardMapper());
    }

    [SetUp]
    public void SetUp()
    {
        _unitOfWork.Reset();
    }

    [Test]
    public async Task GetBoards_ShouldReturnListOfBoardDto()
    {
        // Arrange
        var boards = new List<Board>
        {
            new()
            {
                BoardId = Guid.NewGuid(),
                Name = "Board 1",
                Columns = []
            },
            new()
            {
                BoardId = Guid.NewGuid(),
                Name = "Board 2",
                Columns = []
            }
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetAllAsync(It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns(boards);

        // Act
        var result = await _sut.GetBoards();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Name, Is.EqualTo("Board 1"));
            Assert.That(result[1].Name, Is.EqualTo("Board 2"));
        });
    }

    [Test]
    public async Task CreateBoard_ShouldReturnBoardDto_WhenSuccessful()
    {
        // Arrange
        var createBoardDto = new CreateBoardDto("Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.BoardExists(It.IsAny<string>()).Result)
            .Returns(false);

        // Act
        var result = await _sut.CreateBoard(createBoardDto);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Board 1"));
    }

    [Test]
    public void CreateBoard_ShouldThrowInvalidOperationException_WhenBoardExists()
    {
        // Arrange
        var createBoardDto = new CreateBoardDto("Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.BoardExists(It.IsAny<string>()).Result)
            .Returns(true);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _sut.CreateBoard(createBoardDto));
    }

    [Test]
    public async Task UpdateBoard_ShouldReturnBoardDto_WhenSuccessful()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var updateBoardDto = new UpdateBoardDto(boardId, "Board 1");

        var board = new Board
        {
            BoardId = boardId,
            Name = "Old Board"
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(It.IsAny<Guid>()).Result)
            .Returns(board);

        // Act
        var result = await _sut.UpdateBoard(updateBoardDto);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Board 1"));
    }

    [Test]
    public void UpdateBoard_ShouldThrowInvalidOperationException_WhenBoardNotFound()
    {
        // Arrange
        var updateBoardDto = new UpdateBoardDto(Guid.NewGuid(), "Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(It.IsAny<Guid>()).Result)
            .Returns<Board>(null);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _sut.UpdateBoard(updateBoardDto));
    }

    [Test]
    public async Task DeleteBoard_ShouldDeleteBoard_WhenSuccessful()
    {
        // Arrange
        var boardId = Guid.NewGuid();
        var board = new Board
        {
            BoardId = boardId,
            Name = "Board 1",
            Columns = new List<Column>()
        };

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns(board);

        // Act
        await _sut.DeleteBoard(boardId);

        // Assert
        _unitOfWork.Verify(x => x.BoardsRepository.Remove(board), Times.Once);
    }

    [Test]
    public void DeleteBoard_ShouldThrowInvalidOperationException_WhenBoardNotFound()
    {
        // Arrange
        var boardId = Guid.NewGuid();

        _unitOfWork
            .Setup(x => x.BoardsRepository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns<Board>(null);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _sut.DeleteBoard(boardId));
    }

    [Test]
    public void DeleteBoard_ShouldThrowInvalidOperationException_WhenBoardHasCards()
    {
        // Arrange
        var boardId = Guid.NewGuid();
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
        Assert.ThrowsAsync<InvalidOperationException>(() => _sut.DeleteBoard(boardId));
    }
}