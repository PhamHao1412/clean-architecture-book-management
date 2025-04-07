namespace My_Movie.DTO
{

    public class BookResponse
    {
        public int id { get; set; }
        public string titleM { get; set; }
        public string isbn { get; set; }
        public int pageCount { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
        public DateTime creatAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        public BookResponse(){} 
        public BookResponse(int id, string titleM, string isbn, int pageCount, List<string> Authors)
        {
            this.id = id;
            this.titleM = titleM;
            this.isbn = isbn;
            this.pageCount = pageCount;
            this.Authors = Authors;
        }
    }
}
