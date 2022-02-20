using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation.Validators
{
    public class AssignBookToMemberDtoValidator : AbstractValidator<AssignBookToMemberDto>
    {
        public AssignBookToMemberDtoValidator()
        {
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
