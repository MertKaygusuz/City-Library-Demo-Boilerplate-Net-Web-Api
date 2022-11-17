using FluentValidation;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryApi.Dtos.BookReservation.Validators
{
    public class AssignBookToMemberDtoValidator : AbstractValidator<AssignBookToMemberDto>
    {
        public AssignBookToMemberDtoValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(_ => localizer["User_Name_Required"])
                .NotEmpty()
                .WithMessage(_ => localizer["User_Name_Required"]);
        }
    }
}
