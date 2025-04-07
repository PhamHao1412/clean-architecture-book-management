using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Movie.Domain.Model
{
    public class RandomID
    {
        [Key]
        public int id { get; set; }
        public bool isUse { get; set; }
    }
}
