namespace My_Movie.Model
{
    public class User
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime lastLogged { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<UserBooks> UserBooks { get; set; } = new List<UserBooks>();
        public ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
        public string user_id { get; set; }

    }
}
