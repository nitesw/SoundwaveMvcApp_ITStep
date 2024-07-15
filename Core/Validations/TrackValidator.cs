using FluentValidation;
using Data.Data;
using Core.Dtos;
using Data.Entities;

namespace Core.Validations
{
    public class TrackValidator : AbstractValidator<TrackDto>
    {
        public TrackValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100)
                .Must(UniqueTitle).WithMessage("The track with this title is already exists.");
            RuleFor(x => x.TrackUrl)
                .NotNull()
                .NotEmpty()
                .Must(LinkMustBeAUri).WithMessage("The URL must be valid.");
            RuleFor(x => x.ImgUrl)
                .NotNull()
                .NotEmpty()
                .Must(LinkMustBeAUri).WithMessage("The URL must be valid.");
            RuleFor(x => x.AdditionalTags)
                .MaximumLength(150);
            RuleFor(x => x.ArtistName)
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }

        private bool UniqueTitle(TrackDto track, string title)
        {
            SoundwaveDbContext ctx = new SoundwaveDbContext();
            var dbTitle = ctx.Tracks
                .Where(x => x.Title == title)
                .SingleOrDefault();

            if (dbTitle == null)
                return true;

            return track.Id == dbTitle.Id;
        }
        private static bool LinkMustBeAUri(string? link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
