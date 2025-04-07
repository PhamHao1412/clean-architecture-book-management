using Moq;
using My_Movie.Application.BookFeatures.Queries;
using My_Movie.Application.Exceptions;
using My_Movie.Application.Features.Book.Queries.GetBookByID;
using My_Movie.DTO;
using My_Movie.IRepository;
using Xunit;

namespace My_Movie.Tests.Books;

public class GetBookByIdQueryTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly GetProductByIdQueryHandler _handler;
    public GetBookByIdQueryTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new GetProductByIdQueryHandler(_mockBookRepository.Object);
    }
    
    [Fact]
    public async Task Handle_BookExists_ReturnsBookResponse()
    {
        // Arrange
        var bookId = 1;
        var book = new BookResponse { id = bookId, titleM = "Test Book" }; // Assuming BookResponse has Id and Title properties
        _mockBookRepository.Setup(repo => repo.GetBookByIdAsync(bookId)).ReturnsAsync(book);
        var request = new GetBookByIdQuery(bookId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Code);
        Assert.Equal("success", result.Status);
        Assert.Equal(book, result.Data);
    }
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = 1;
    
        var bookRepositoryMock = new Mock<IBookRepository>();
        bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(bookId))
            .ReturnsAsync((BookResponse)null); // Trả về null để giả lập sách không tồn tại
    
        var handler = new GetProductByIdQueryHandler(bookRepositoryMock.Object);
        var cancellationToken = new CancellationToken();
    
        var query = new GetBookByIdQuery(bookId);
    
        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            async () => await handler.Handle(query, cancellationToken)
        );
    
        Assert.Equal($"The book with ID {bookId} was not found.", exception.Message);
        bookRepositoryMock.Verify(repo => repo.GetBookByIdAsync(bookId), Times.Once);
    }
}