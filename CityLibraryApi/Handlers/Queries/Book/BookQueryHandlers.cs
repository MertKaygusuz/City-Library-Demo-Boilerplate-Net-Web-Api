using CityLibraryApi.Queries.Book;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Queries.Book
{
    public class BookQueryHandlers : IRequestHandler<GetAllBooksQuery, IEnumerable<Books>>, 
                                     IRequestHandler<GetNumberOfAuthorsFromBookTableQuery, int>,
                                     IRequestHandler<GetNumberOfDistinctTitleQuery, int>
    {
        private readonly IBooksRepo _booksRepo;
        public BookQueryHandlers(IBooksRepo booksRepo)
        {
            _booksRepo = booksRepo;
        }
        public async Task<IEnumerable<Books>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepo.GetData().ToListAsync(cancellationToken);
        }

        public async Task<int> Handle(GetNumberOfDistinctTitleQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepo.GetData().Select(x => x.BookTitle).Distinct().CountAsync(cancellationToken);
        }

        public async Task<int> Handle(GetNumberOfAuthorsFromBookTableQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepo.GetData().Select(x => x.Author).Distinct().CountAsync(cancellationToken);
        }
    }
}
