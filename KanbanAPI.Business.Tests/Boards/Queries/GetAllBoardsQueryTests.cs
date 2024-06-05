using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.Business.Boards.Queries.GetAllBoards;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Moq;

namespace KanbanAPI.Business.Tests.Boards.Queries;

[TestFixture]
public class GetAllBoardsQueryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly GetAllBoardsQueryHandler _handler;

    public GetAllBoardsQueryTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new GetAllBoardsQueryHandler(_unitOfWork.Object, new BoardMapper());
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
            .Setup(u => u.BoardsRepository.GetAllAsync(It.IsAny<ContextGetParameters<Board>>()).Result)
            .Returns(boards);

        // Act
        var result = await _handler.Handle(new GetAllBoardsQuery(), CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Name, Is.EqualTo("Board 1"));
            Assert.That(result[1].Name, Is.EqualTo("Board 2"));
        });
    }
}