using FluentValidation;

namespace CityLibraryApi.Dtos.Authentication.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
