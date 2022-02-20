using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation.Validators
{
    public class ReservationHistoryBookDtoValidator : AbstractValidator<ReservationHistoryBookDto>
    {
        public ReservationHistoryBookDtoValidator()
        {
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
