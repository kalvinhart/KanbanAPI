using KanbanAPI.Boards.Controllers;
using KanbanAPI.Business.Boards.Commands.CreateBoard;
using KanbanAPI.Business.Boards.Commands.DeleteBoard;
using KanbanAPI.Business.Boards.Commands.UpdateBoard;
using KanbanAPI.Business.Boards.DTOs;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Boards.Queries.GetAllBoards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KanbanAPI.Tests.Boards.Controllers;

[TestFixture]
public class BoardsControllerTests
{
    private readonly Mock<ISender> _sender;
    private readonly BoardsController _sut;

    public BoardsControllerTests()
    {
        _sender = new Mock<ISender>();
        _sut = new BoardsController(_sender.Object, new BoardMapper());
    }

    [SetUp]
    public void SetUp()
    {
        _sender.Reset();
    }

    [Test]
    public async Task GetBoards_ShouldReturnBoards()
    {
        // Arrange
        var boards = new List<BoardDto>
        {
            new (Guid.NewGuid(), "Board 1", []),
            new (Guid.NewGuid(), "Board 2", [])
        };

        _sender
            .Setup(x => x.Send(It.IsAny<GetAllBoardsQuery>(), It.IsAny<CancellationToken>()).Result)
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

        _sender
            .Setup(x => x.Send(
                It.IsAny<CreateBoardCommand>(),
                It.IsAny<CancellationToken>()).Result)
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

        _sender
            .Setup(x => x.Send(
                It.IsAny<UpdateBoardCommand>(),
                It.IsAny<CancellationToken>()).Result)
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

        _sender
            .Setup(x => x.Send(
                It.IsAny<UpdateBoardCommand>(),
                It.IsAny<CancellationToken>()).Result)
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

        _sender
            .Setup(x => x.Send(
                It.IsAny<DeleteBoardCommand>(),
                It.IsAny<CancellationToken>()).Result)
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

        _sender
            .Setup(x => x.Send(
                It.IsAny<DeleteBoardCommand>(),
                It.IsAny<CancellationToken>()).Result)
            .Returns((bool?)null);

        // Act
        var actionResult = await _sut.DeleteBoard(boardId);

        // Assert
        Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
    }
}