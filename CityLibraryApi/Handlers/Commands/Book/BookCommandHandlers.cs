using CityLibraryApi.Commands.Book;
using CityLibraryApi.Dtos.Book;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.MapperConfigurations;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Commands.Book
{
    public class BookCommandHandlers : IRequestHandler<BookUpdateCommand>,
                                       IRequestHandler<BookRegisterCommand, int>,
                                       IRequestHandler<BookDeleteCommand>
    {
        private readonly IBooksRepo _booksRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        public BookCommandHandlers(IBooksRepo booksRepo, IUnitOfWork unitOfWork, ICustomMapper customMapper)
        {
            _booksRepo = booksRepo;
            _unitOfWork = unitOfWork;
            _mapper = customMapper;
        }

        public async Task<Unit> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _booksRepo.GetByIdAsync(request.Dto.BookId);
            if(existingBook is null)
                throw new CustomStatusException("Book was not found!", 404);
            _mapper.MapToExistingObject(request.Dto, existingBook);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

        public async Task<int> Handle(BookRegisterCommand request, CancellationToken cancellationToken)
        {
            var bookToAdd = _mapper.Map<RegisterBookDto, Books>(request.Dto);
            await _booksRepo.InsertAsync(bookToAdd);
            await _unitOfWork.CommitAsync();
            return bookToAdd.BookId;
        }

        public async Task<Unit> Handle(BookDeleteCommand request, CancellationToken cancellationToken)
        {
            var theBook = await _booksRepo.GetByIdAsync(request.Dto.BookId);
            if (theBook is null)
                throw new CustomStatusException("Book was not found!", 404);

            _booksRepo.Delete(theBook);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
