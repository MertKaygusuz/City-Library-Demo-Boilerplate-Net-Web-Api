using CityLibraryApi.Commands.Book;
using CityLibraryApi.Dtos.Book;
using CityLibraryApi.Queries.Book;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers.Book
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<int> BookRegister(RegisterBookDto dto)
        {
            return await _mediator.Send(new BookRegisterCommand(dto));
        }

        [HttpPut]
        public async Task<IActionResult> BookInfoUpdate(UpdateBookDto dto)
        {
            await _mediator.Send(new BookRegisterCommand(dto));
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(DeleteBookDto dto)
        {
            await _mediator.Send(new BookDeleteCommand(dto));
            return Ok();
        }
    }
}
