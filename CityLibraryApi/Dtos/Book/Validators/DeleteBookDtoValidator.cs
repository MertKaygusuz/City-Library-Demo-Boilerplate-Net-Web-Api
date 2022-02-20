using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Book.Validators
{
    public class DeleteBookDtoValidator : AbstractValidator<DeleteBookDto>
    {
        public DeleteBookDtoValidator()
        {
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
