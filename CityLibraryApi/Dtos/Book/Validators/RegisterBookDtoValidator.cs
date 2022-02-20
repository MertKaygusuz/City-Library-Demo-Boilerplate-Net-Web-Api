using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.Book.Validators
{
    public class RegisterBookDtoValidator : AbstractValidator<RegisterBookDto>
    {
        public RegisterBookDtoValidator()
        {
            RuleFor(x => x.Author).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.BookTitle).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.FirstPublishDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.EditionNumber).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.EditionDate).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.AvailableCount).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
            RuleFor(x => x.CoverType).IsInEnum().NotEmpty();
            RuleFor(x => x.TitleType).IsInEnum().NotEmpty();
        }
    }
}
