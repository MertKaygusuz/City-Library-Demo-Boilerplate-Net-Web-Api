using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Book.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            Include(new RegisterBookDtoValidator());
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
