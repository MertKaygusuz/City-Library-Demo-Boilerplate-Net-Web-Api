using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Member.Validators
{
    public class MemberSelfUpdateDtoValidator : AbstractValidator<MemberSelfUpdateDto>
    {
        public MemberSelfUpdateDtoValidator()
        {
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(8);
            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(x => x.BirthDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
