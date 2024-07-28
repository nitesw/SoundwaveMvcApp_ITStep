namespace Core.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Likes { get; set; }
        public int Playlists { get; set; }
        public bool IsAdmin { get; set; }
    }
}
