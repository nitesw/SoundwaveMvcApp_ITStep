using Microsoft.EntityFrameworkCore;
using Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Data
{
    public class SoundwaveDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SoundwaveMVC_DB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasData(new List<User>()
            {
                new User() { Id = 1, Username = "admin", Email = "admin@mail.com", Password = "adminpass", Likes = 0, Playlists = 0, IsAdmin = true},
                new User() { Id = 2, Username = "damnsss", Email = "damnsss@mail.com", Password = "1234pass", Likes = 0, Playlists = 1, IsAdmin = false},
                new User() { Id = 3, Username = "uzibook", Email = "uzibook@mail.com", Password = "passsword1234", Likes = 12, Playlists = 2, IsAdmin = false},
                new User() { Id = 4, Username = "zxcnewr", Email = "zxcnewr@mail.com", Password = "pass1234", Likes = 0, Playlists = 0, IsAdmin = false},
                new User() { Id = 5, Username = "Moomaszh", Email = "moomaszh@mail.com", Password = "pass0000", Likes = 5, Playlists = 0, IsAdmin = false},
            });

            modelBuilder.Entity<Genre>().HasData(new List<Genre>()
            {
               new Genre() { Id = 1, Name = "None" },
                new Genre() { Id = 2, Name = "Alternative Rock" },
                new Genre() { Id = 3, Name = "Ambient" },
                new Genre() { Id = 4, Name = "Classical" },
                new Genre() { Id = 5, Name = "Country" },
                new Genre() { Id = 6, Name = "Dance & EDM" },
                new Genre() { Id = 7, Name = "Dancehall" },
                new Genre() { Id = 8, Name = "Deep House" },
                new Genre() { Id = 9, Name = "Disco" },
                new Genre() { Id = 10, Name = "Drum & Bass" },
                new Genre() { Id = 11, Name = "Dubstep" },
                new Genre() { Id = 12, Name = "Electronic" },
                new Genre() { Id = 13, Name = "Folk & Singer-Songwriter" },
                new Genre() { Id = 14, Name = "Hip-hop & Rap" },
                new Genre() { Id = 15, Name = "House" },
                new Genre() { Id = 16, Name = "Indie" },
                new Genre() { Id = 17, Name = "Jazz & Blues" },
                new Genre() { Id = 18, Name = "Latin" },
                new Genre() { Id = 19, Name = "Metal" },
                new Genre() { Id = 20, Name = "Piano" },
                new Genre() { Id = 21, Name = "Pop" },
                new Genre() { Id = 22, Name = "R&B & Soul" },
                new Genre() { Id = 23, Name = "Reggae" },
                new Genre() { Id = 24, Name = "Reggaeton" },
                new Genre() { Id = 25, Name = "Rock" },
                new Genre() { Id = 26, Name = "Soundtrack" },
                new Genre() { Id = 27, Name = "Techno" },
                new Genre() { Id = 28, Name = "Trance" },
                new Genre() { Id = 29, Name = "Trap" },
                new Genre() { Id = 30, Name = "Triphop" },
                new Genre() { Id = 31, Name = "World" }
            });

            modelBuilder.Entity<Track>().HasIndex(s => s.Title).IsUnique();
            modelBuilder.Entity<Track>().HasData(new List<Track>()
            {
                new Track() {Id = 1, Title = "Test Song", GenreId = 2, IsPublic = true, TrackUrl = "randomsite.com/songurl.mp3", ImgUrl="https://i.redd.it/lhg9d9b80lz61.png", UserId = 1, UploadDate = DateTime.Now.Date, AdditionalTags = "true, tags", ArtistName = "Me", Description = "True test music" },
                new Track() {Id = 2, Title = "Test Song 2", GenreId = 1, IsPublic = false, TrackUrl = "aaa.com/mp3", ImgUrl="https://preview.redd.it/o94pn5h60lz61.png?width=1080&crop=smart&auto=webp&s=7464db335ee53167d2f6e2288d162711ed0a31d1", UserId = 2, UploadDate = DateTime.Now.Date }
            });
        }  
    }
}
