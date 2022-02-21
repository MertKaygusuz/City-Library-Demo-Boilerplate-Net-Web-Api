using CityLibraryApi.Dtos.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Commands.Book
{
    public record BookUpdateCommand(UpdateBookDto Dto) : IRequest
    {
    }
}
