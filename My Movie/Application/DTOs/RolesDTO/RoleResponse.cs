namespace My_Movie.DTO
{
    public class RoleResponse
    {
        public string Name { get; set; }
        public DateTime createAt { get; set; } = DateTime.UtcNow;

        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    }
}