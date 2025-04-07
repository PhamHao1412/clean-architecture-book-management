using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;
using Polly;

namespace My_Movie.Infrastructure.Repositiory;

public class BookRepository : IBookRepository
{
    private readonly DBContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<BookRepository> _logger;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    public BookRepository(DBContext dbContext, IMapper mapper, ILogger<BookRepository> logger, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _cache = cache;
    }

    public async Task<List<BookResponse>> GetAllBooksCache()
    {
        var cacheKey = "books";
        _logger.LogInformation("fetching data for key: {CacheKey} from cache.", cacheKey);
        if (!_cache.TryGetValue(cacheKey, out List<Book>? books))
        {
            _logger.LogInformation("cache miss. fetching data for key: {CacheKey} from database.", cacheKey);
            books = await _dbContext.Books.ToListAsync();
            if (books.LongCount() == 0 || books == null)  throw new NotFoundException("Books was not found.");
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                .SetPriority(CacheItemPriority.NeverRemove)
                .SetSize(2048);
            _logger.LogInformation("setting data for key: {CacheKey} to cache.", cacheKey);
            _cache.Set(cacheKey, books, cacheOptions);
        }
        else
        {
            _logger.LogInformation("cache hit for key: {CacheKey}.", cacheKey);
        }

        return _mapper.Map<List<BookResponse>>(books);
    }

    public async Task<List<BookResponse>> GetAllBooks()
    {
        var books = await _dbContext.Books.ToListAsync();
        if (books.Count == 0)
            return [];
        return _mapper.Map<List<BookResponse>>(books);
    }


    public async Task<List<BookResponse>> GetFavoriteBooks(int user_id)
    {
        _logger.LogInformation("User with ID {user_id} is getting favourite books", user_id);
        var userBooks = await _dbContext.UserBooks
            .Where(ub => ub.UserId == user_id)
            .Include(ub => ub.Book)
            .Select(ub => ub.Book)
            .ToListAsync();
        if (userBooks.Count == 0)
        {
            _logger.LogInformation("Favourite books of User with ID {user_id} are empty", user_id);
            throw new NotFoundException("Not found your favorite book");
        }

        _logger.LogInformation("User with ID {user_id} has got favourite books", user_id);

        var bookDTOs = _mapper.Map<List<BookResponse>>(userBooks);
        return bookDTOs;
    }
     async Task<IEnumerable<BookResponse>> GetBookByAuthors(string authors)
    {
        if (string.IsNullOrEmpty(authors))
        {
            var errorMessage = "No author name provided";
            return null;
        }

        var books = await _dbContext.Books
            .Where(b => b.Authors.Contains(authors))
            .ToListAsync();


        return _mapper.Map<IEnumerable<BookResponse>>(books);
    }

    public async Task<Book> GetBookById(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book == null)
        {
            _logger.LogInformation("Book with ID {BookId} was not found", id);
            throw new NotFoundException($"Book with ID {id} was not found", id);
        }

        return book;
    }

    public async Task<BookResponse> GetBookByIdAsync(int id)
    {
        _logger.LogInformation($"The book with ID {id} is getting ", id);

        var book = await _dbContext.Books.FindAsync(id);
        if (book == null)
        {
            _logger.LogInformation("Book with ID {BookId} was not found", id);
            throw new NotFoundException($"Book with ID {id} was not found", id);
        }

        _logger.LogInformation($"The book with ID {id} has got successfully ", id);

        var bookResponse = _mapper.Map<BookResponse>(book);
        return bookResponse;
    }

    // public async Task LogBookAccessAsync(int bookId, string additionalInfo)
    // {
    //     // Perform any additional processing, logging, etc.
    //     Console.WriteLine($"Background job: Accessed book with ID: {bookId}, Info: {additionalInfo}");
    //     // You can also perform database operations here if needed
    // }

    public async Task<BookResponse> AddBookAsync(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<BookResponse>(book);
    }

    // public async Task AddBookAsync(Book book)   
    // {
    //     _dbContext.Books.Add(book);
    //     await _dbContext.SaveChangesAsync();
    // }

    public async Task<BookResponse> UpdateBookAsync(Book updateBook)
    {
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<BookResponse>(updateBook);
    }

    public async Task DeleteBookAsync(Book book)
    {
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserBooks?> GetFavouriteBooks(int user_id, int book_id)
    {
        var userBooks = await _dbContext.UserBooks
            .FirstOrDefaultAsync(b => b.UserId == user_id && b.BookId == book_id);
        if (userBooks != null)
        {
            _logger.LogInformation(
                "The book with ID {BookId} is already added to the list of favorite books for user with ID {UserId}}"
                , book_id, user_id);
            throw new BadRequestException("This book is already added to your list of favorites.");
        }

        return userBooks;
    }

    public async Task AddFavouriteBook(UserBooks userBook)
    {
        var existingBook = await _dbContext.UserBooks
            .FirstOrDefaultAsync(b => b.UserId == userBook.UserId && b.BookId == userBook.BookId);
        if (existingBook == null)
        {
            _dbContext.UserBooks.Add(userBook);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation(
                "The book with ID {BookId} is already added to the list of favorite books for user with ID {UserId}}"
                , userBook.BookId, userBook.UserId);
            throw new BadRequestException("This book is already added to your list of favorites.");
        }
    }

    public async Task RemoveFavoriteBook(int user_id, int book_id)
    {
        var favoriteBook = await _dbContext.UserBooks
            .FirstOrDefaultAsync(b => b.UserId == user_id && b.BookId == book_id);
        if (favoriteBook != null)
        {
            _dbContext.UserBooks.Remove(favoriteBook);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation(
                "The book with ID {BookId} not exist in the list of favorite books for user with ID {UserId}}"
                , favoriteBook.BookId, favoriteBook.UserId);
            throw new NotFoundException(
                $"The book with ID {favoriteBook.BookId} was not found in your list of favorites", favoriteBook.BookId);
        }
    }

    public async Task<PageList<BookResponse>> GetProductsQuery(
        string searchTerm, string sortColumn, string sortOrder,
        int page, int pageSize)
    {
        IQueryable<Book> booksQuery = _dbContext.Books;
        if (!string.IsNullOrWhiteSpace(searchTerm))
            booksQuery = booksQuery.Where(p => p.title.ToLower().Contains(searchTerm.ToLower()));

        booksQuery = sortOrder?.ToLower() == "desc"
            ? booksQuery.OrderByDescending(GetSortProperty(sortColumn))
            : booksQuery.OrderBy(GetSortProperty(sortColumn));
        var bookResponsesQuery = booksQuery
            .Select(p => new BookResponse(
                p.id,
                p.title,
                p.isbn,
                p.pageCount,
                p.Authors));
        var books = await PageList<BookResponse>.CreateAsync(bookResponsesQuery, page, pageSize);
        return books;
    }

    private static Expression<Func<Book, object>> GetSortProperty(string SortColumn)
    {
        return SortColumn?.ToLower() switch
        {
            "title" => book => book.title,
            "isbn" => book => book.isbn,
            "pageCount" => book => book.pageCount,
            "authors" => book => book.Authors,
            _ => book => book.id
        };
    }
}