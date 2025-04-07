using My_Movie.Application.BookFeatures.Queries;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.IRepository
{
    public interface IBookRepository
    {
        Task<List<BookResponse>> GetAllBooks();
        Task<List<BookResponse>> GetAllBooksCache();
        Task<BookResponse> GetBookByIdAsync(int id);
        Task<Book> GetBookById(int id);
        Task<BookResponse> AddBookAsync(Book book);
        Task<BookResponse> UpdateBookAsync(Book updateBook);
        Task DeleteBookAsync(Book book);
        Task<List<BookResponse>> GetFavoriteBooks(int user_id);
        Task<UserBooks?> GetFavouriteBooks(int user_id, int book_id);
        Task AddFavouriteBook(UserBooks userBook);
        Task RemoveFavoriteBook(int user_id, int book_id);
        Task<PageList<BookResponse>> GetProductsQuery(string title,string sortColumn, string sortOrder,
            int page, int pageSize);
        
    }
}
