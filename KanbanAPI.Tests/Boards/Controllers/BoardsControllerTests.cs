using KanbanAPI.Boards.Controllers;
using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KanbanAPI.Tests.Boards.Controllers;

[TestFixture]
public class BoardsControllerTests
{
    private readonly Mock<IBoardsService> _boardsService;
    private readonly BoardsController _sut;

    public BoardsControllerTests()
    {
        _boardsService = new Mock<IBoardsService>();
        _sut = new BoardsController(_boardsService.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _boardsService.Reset();
    }

    [Test]
    public async Task GetBoards_ShouldReturnBoards()
    {
        // Arrange
        var boards = new List<BoardDto>
        {
            new BoardDto(Guid.NewGuid(), "Board 1", []),
            new BoardDto(Guid.NewGuid(), "Board 2", [])
        };

        _boardsService
            .Setup(x => x.GetBoards().Result)
            .Returns(boards);

        // Act
        var actionResult = await _sut.GetBoards();
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Value, Has.Count.EqualTo(2));
            Assert.That(result.Value, Is.EqualTo(boards));
        });
    }

    [Test]
    public async Task CreateBoard_ShouldReturnCreatedBoard()
    {
        // Arrange
        var createBoardDto = new CreateBoardDto("Board 1");
        var board = new BoardDto(Guid.NewGuid(), "Board 1", []);

        _boardsService
            .Setup(x => x.CreateBoard(createBoardDto).Result)
            .Returns(board);

        // Act
        var actionResult = await _sut.CreateBoard(createBoardDto);
        var result = actionResult.Result as CreatedAtActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Value, Is.EqualTo(board));
        });
    }

    [Test]
    public async Task UpdateBoard_ShouldReturnUpdatedBoard()
    {
        // Arrange
        var updateBoardDto = new UpdateBoardDto(Guid.NewGuid(), "Board 1");
        var board = new BoardDto(Guid.NewGuid(), "Board 1", []);

        _boardsService
            .Setup(x => x.UpdateBoard(updateBoardDto).Result)
            .Returns(board);

        // Act
        var actionResult = await _sut.UpdateBoard(updateBoardDto);
        var result = actionResult.Result as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Value, Is.EqualTo(board));
        });
    }

    [Test]
    public async Task UpdateBoard_ShouldReturnNotFound()
    {
        // Arrange
        var updateBoardDto = new UpdateBoardDto(Guid.NewGuid(), "Board 1");

        _boardsService
            .Setup(x => x.UpdateBoard(updateBoardDto).Result)
            .Returns((BoardDto?)null);

        // Act
        var actionResult = await _sut.UpdateBoard(updateBoardDto);

        // Assert
        Assert.That(actionResult.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteBoard_ShouldReturnNoContent()
    {
        // Arrange
        var boardId = Guid.NewGuid();

        _boardsService
            .Setup(x => x.DeleteBoard(boardId).Result)
            .Returns(true);

        // Act
        var actionResult = await _sut.DeleteBoard(boardId);
        var result = actionResult as NoContentResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.StatusCode, Is.EqualTo(204));
    }

    [Test]
    public async Task DeleteBoard_ShouldReturnNotFound()
    {
        // Arrange
        var boardId = Guid.NewGuid();

        _boardsService
            .Setup(x => x.DeleteBoard(boardId).Result)
            .Returns((bool?)null);

        // Act
        var actionResult = await _sut.DeleteBoard(boardId);

        // Assert
        Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
    }
}