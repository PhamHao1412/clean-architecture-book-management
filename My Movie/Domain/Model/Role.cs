using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_Movie.Model
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime createAt { get; set; } = DateTime.UtcNow;

        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
    }
}
