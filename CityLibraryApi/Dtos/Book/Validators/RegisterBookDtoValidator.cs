using FluentValidation;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryApi.Dtos.Book.Validators
{
    public class RegisterBookDtoValidator : AbstractValidator<RegisterBookDto>
    {
        public RegisterBookDtoValidator(IStringLocalizer<BookValidationResource> localizer)
        {
            RuleFor(x => x.Author).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Author_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Author_Required"]);
            RuleFor(x => x.BookTitle).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Book_Title_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Book_Title_Required"]);
            RuleFor(x => x.FirstPublishDate).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["First_Publish_Date_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["First_Publish_Date_Required"]);
            RuleFor(x => x.EditionNumber).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Edition_Number_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Edition_Number_Required"]);
            RuleFor(x => x.EditionDate).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Edition_Date_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Edition_Date_Required"]);
            RuleFor(x => x.AvailableCount).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["Available_Count_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["Available_Count_Required"]);
            RuleFor(x => x.CoverType)
                .IsInEnum()
                .WithMessage(_ => localizer["Cover_Type_Invalid"])
                .NotEmpty()
                .WithMessage(_ => localizer["Cover_Type_Required"]);
            RuleFor(x => x.TitleType)
                .IsInEnum()
                .WithMessage(_ => localizer["Title_Type_Invalid"])
                .NotEmpty()
                .WithMessage(_ => localizer["Title_Type_Required"]);
        }
    }
}
