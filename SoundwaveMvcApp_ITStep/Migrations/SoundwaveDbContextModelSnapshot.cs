﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoundwaveMvcApp_ITStep.Data;

#nullable disable

namespace SoundwaveMvcApp_ITStep.Migrations
{
    [DbContext(typeof(SoundwaveDbContext))]
    partial class SoundwaveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "None"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Alternative Rock"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ambient"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Classical"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Country"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Dance & EDM"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Dancehall"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Deep House"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Disco"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Drum & Bass"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Dubstep"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Electronic"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Folk & Singer-Songwriter"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Hip-hop & Rap"
                        },
                        new
                        {
                            Id = 15,
                            Name = "House"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Indie"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Jazz & Blues"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Latin"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Piano"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Pop"
                        },
                        new
                        {
                            Id = 22,
                            Name = "R&B & Soul"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Reggae"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Reggaeton"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Rock"
                        },
                        new
                        {
                            Id = 26,
                            Name = "Soundtrack"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Techno"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Trance"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Trap"
                        },
                        new
                        {
                            Id = 30,
                            Name = "Triphop"
                        },
                        new
                        {
                            Id = 31,
                            Name = "World"
                        });
                });

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalTags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TrackUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Tracks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdditionalTags = "true, tags",
                            ArtistName = "Me",
                            Description = "True test music",
                            GenreId = 2,
                            ImgUrl = "https://i.redd.it/lhg9d9b80lz61.png",
                            IsArchived = false,
                            IsPublic = true,
                            Title = "Test Song",
                            TrackUrl = "randomsite.com/songurl.mp3",
                            UploadDate = new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 1,
                            ImgUrl = "https://preview.redd.it/o94pn5h60lz61.png?width=1080&crop=smart&auto=webp&s=7464db335ee53167d2f6e2288d162711ed0a31d1",
                            IsArchived = false,
                            IsPublic = false,
                            Title = "Test Song 2",
                            TrackUrl = "aaa.com/mp3",
                            UploadDate = new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Playlists")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail.com",
                            IsAdmin = true,
                            Likes = 0,
                            Password = "adminpass",
                            Playlists = 0,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "damnsss@mail.com",
                            IsAdmin = false,
                            Likes = 0,
                            Password = "1234pass",
                            Playlists = 1,
                            Username = "damnsss"
                        },
                        new
                        {
                            Id = 3,
                            Email = "uzibook@mail.com",
                            IsAdmin = false,
                            Likes = 12,
                            Password = "passsword1234",
                            Playlists = 2,
                            Username = "uzibook"
                        },
                        new
                        {
                            Id = 4,
                            Email = "zxcnewr@mail.com",
                            IsAdmin = false,
                            Likes = 0,
                            Password = "pass1234",
                            Playlists = 0,
                            Username = "zxcnewr"
                        },
                        new
                        {
                            Id = 5,
                            Email = "moomaszh@mail.com",
                            IsAdmin = false,
                            Likes = 5,
                            Password = "pass0000",
                            Playlists = 0,
                            Username = "Moomaszh"
                        });
                });

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.Track", b =>
                {
                    b.HasOne("SoundwaveMvcApp_ITStep.Entities.Genre", "Genre")
                        .WithMany("Tracks")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoundwaveMvcApp_ITStep.Entities.User", "User")
                        .WithMany("Tracks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.Genre", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("SoundwaveMvcApp_ITStep.Entities.User", b =>
                {
                    b.Navigation("Tracks");
                });
#pragma warning restore 612, 618
        }
    }
}
