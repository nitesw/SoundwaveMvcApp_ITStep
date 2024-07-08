namespace SoundwaveMvcApp_ITStep.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string SongUrl { get; set; }
        public bool IsPublic { get; set; }
        public string? AdditionalTags { get; set; }
        public DateTime UploadDate { get; set; }
        public string? ArtistName { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
