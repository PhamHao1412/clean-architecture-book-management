using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_Movie.Model
{
    public class Book 
    {
        public Book(){}

        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public int pageCount { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
        public DateTime creatAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; }= DateTime.UtcNow;
        public ICollection<UserBooks> UserBooks { get; set; } = new List<UserBooks>();
        

    }
}
