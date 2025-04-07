using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Moq;
using My_Movie.Application.BookFeatures.CommandHandlers;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.Application.Features.Book.Commands.POST.CreateBook;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;
using Xunit;

namespace My_Movie.Tests.Books;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<CreateBookCommandHandler>> _loggerMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<CreateBookCommandHandler>>();

        _handler = new CreateBookCommandHandler(
            _bookRepositoryMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCreateBook_WhenCommandIsValid()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim("user_id", "7")
        }));

        _userRepositoryMock.Setup(repo => repo.GetAuthenticatedUser())
            .Returns(userClaims);

        var createBookCommand = new CreateBookCommand("Test Title", "1241232-ei", 200,new List<string> { "Author1", "Author2" });

        var bookResponse = new BookResponse
        {
            titleM = createBookCommand.title,
            isbn = createBookCommand.isbn,
            pageCount = createBookCommand.pageCount,
            Authors = createBookCommand.Authors,
            creatAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        };

        _bookRepositoryMock.Setup(repo => repo.AddBookAsync(It.IsAny<Book>()))
            .ReturnsAsync(bookResponse);

        var cancellationToken = new CancellationToken();

        // Act
        var result = await _handler.Handle(createBookCommand, cancellationToken);

        // Assert
        Assert.Equal(200, result.Code);
        Assert.Equal("success", result.Status);
        Assert.NotNull(result.Data);
        Assert.Equal(createBookCommand.title, result.Data.titleM);
        Assert.Equal(createBookCommand.isbn, result.Data.isbn);
        Assert.Equal(createBookCommand.pageCount, result.Data.pageCount);
        Assert.Equal(createBookCommand.Authors, result.Data.Authors);

        _userRepositoryMock.Verify(repo => repo.GetAuthenticatedUser(), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.AddBookAsync(It.IsAny<Book>()), Times.Once);
    }

}