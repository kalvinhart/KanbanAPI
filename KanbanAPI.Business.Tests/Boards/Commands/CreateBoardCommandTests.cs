using KanbanAPI.Business.Boards.Commands.CreateBoard;
using KanbanAPI.Business.Boards.Mapping;
using KanbanAPI.DataAccess.Shared.UnitOfWork;
using Moq;

namespace KanbanAPI.Business.Tests.Boards.Commands;

[TestFixture]
public class CreateBoardCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateBoardCommandHandler _handler;

    public CreateBoardCommandTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateBoardCommandHandler(_unitOfWork.Object, new BoardMapper());
    }

    [SetUp]
    public void SetUp()
    {
        _unitOfWork.Reset();
    }

    [Test]
    public async Task CreateBoard_ShouldReturnBoardDto_WhenSuccessful()
    {
        // Arrange
        var command = new CreateBoardCommand("Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.BoardExists(It.IsAny<string>()).Result)
            .Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Board 1"));
    }

    [Test]
    public void CreateBoard_ShouldThrowInvalidOperationException_WhenBoardExists()
    {
        // Arrange
        var command = new CreateBoardCommand("Board 1");

        _unitOfWork
            .Setup(x => x.BoardsRepository.BoardExists(It.IsAny<string>()).Result)
            .Returns(true);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}