using FluentValidation;

namespace CityLibraryApi.Dtos.Member.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(8);
            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(x => x.BirthDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
