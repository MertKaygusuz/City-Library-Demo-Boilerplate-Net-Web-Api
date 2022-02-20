using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation.Validators
{
    public class ReservationHistoryMemberDtoValidator : AbstractValidator<ReservationHistoryMemberDto>
    {
        public ReservationHistoryMemberDtoValidator()
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
