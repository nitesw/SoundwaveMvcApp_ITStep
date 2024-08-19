using Core.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validations
{
    public class PlaylistValidator : AbstractValidator<PlaylistDto>
    {
        public PlaylistValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x.ImgUrl)
                .NotEmpty()
                .NotNull()
                .Must(LinkMustBeAUri).WithMessage("The URL must be valid.");
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
