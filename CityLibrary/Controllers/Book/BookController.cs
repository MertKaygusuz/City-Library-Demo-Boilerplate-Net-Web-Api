using CityLibrary.ActionFilters.Base;
using CityLibraryApi.Dtos.Book;
using CityLibraryApi.Services.Book.Interfaces;
using CityLibraryInfrastructure.Entities;
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
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooks();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<int> BookRegister(RegisterBookDto dto)
        {
            return await _bookService.BookRegisterAsync(dto);
        }

        [HttpPut]
        [ServiceFilter(typeof(GenericNotFoundFilter<IBookService>))]
        public async Task<IActionResult> BookInfoUpdate(UpdateBookDto dto)
        {
            await _bookService.UpdateBookInfoAsync(dto);
            return Ok();
        }

        [HttpDelete]
        [ServiceFilter(typeof(GenericNotFoundFilter<IBookService>))]
        public async Task<IActionResult> DeleteBook(DeleteBookDto dto)
        {
            await _bookService.DeleteBookAsync(dto);
            return Ok();
        }
    }
}
