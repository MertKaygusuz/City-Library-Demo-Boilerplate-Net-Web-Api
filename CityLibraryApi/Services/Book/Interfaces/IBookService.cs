using CityLibraryApi.Dtos.Book;
using CityLibraryInfrastructure.BaseInterfaces;
using CityLibraryInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Services.Book.Interfaces
{
    public interface IBookService : IBaseCheckService
    {
        /// <summary>
        /// Save new books
        /// </summary>
        /// <param name="dto">Registration parameters</param>
        /// <returns>Book id</returns>
        Task<int> BookRegisterAsync(RegisterBookDto dto);

        /// <summary>
        /// Update book's information of existing book
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateBookInfoAsync(UpdateBookDto dto);

        /// <summary>
        /// Soft delete for book records
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task DeleteBookAsync(DeleteBookDto dto);

        Task<int> GetNumberOfDistinctTitleAsync();

        Task<int> GetNumberOfAuthorsFromBookTableAsync();

        Task<IEnumerable<Books>> GetAllBooks();
    }
}
