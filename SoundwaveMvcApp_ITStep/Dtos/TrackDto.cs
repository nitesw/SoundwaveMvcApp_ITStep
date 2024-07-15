namespace SoundwaveMvcApp_ITStep.Dtos
{
    public class TrackDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TrackUrl { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool IsArchived { get; set; }
        public string? AdditionalTags { get; set; }
        public DateTime UploadDate { get; set; }
        public string? ArtistName { get; set; }

        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public int UserId { get; set; }
        public string? UserUsername { get; set; }
    }
}
