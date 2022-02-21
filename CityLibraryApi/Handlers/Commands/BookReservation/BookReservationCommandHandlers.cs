using CityLibraryApi.Commands.BookReservation;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Commands.BookReservation
{
    public class BookReservationCommandHandlers : IRequestHandler<AssignBookToMemberCommand>,
                                                  IRequestHandler<UnAssignBookFromUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActiveBookReservationsRepo _activeBookReservationsRepo;
        private readonly IBookReservationHistoriesRepo _bookReservationHistoriesRepo;
        private readonly IMembersRepo _membersRepo;
        private readonly IBooksRepo _booksRepo;
        public BookReservationCommandHandlers(
            IUnitOfWork unitOfWork,
            IActiveBookReservationsRepo activeBookReservationsRepo,
            IBookReservationHistoriesRepo bookReservationHistoriesRepo,
            IMembersRepo membersRepo,
            IBooksRepo booksRepo)
        {
            _unitOfWork = unitOfWork;
            _activeBookReservationsRepo = activeBookReservationsRepo;
            _bookReservationHistoriesRepo = bookReservationHistoriesRepo;
            _membersRepo = membersRepo;
            _booksRepo = booksRepo;
        }

        public async Task<Unit> Handle(UnAssignBookFromUserCommand request, CancellationToken cancellationToken)
        {
            await CheckMemberAndBookAsync(request.Dto.UserName, request.Dto.BookId);
            var theBook = await _booksRepo.GetByIdAsync(request.Dto.BookId);
            theBook.AvailableCount -= 1;
            theBook.ReservedCount += 1;
            ActiveBookReservations reservation = new()
            {
                BookId = request.Dto.BookId,
                MemberId =  request.Dto.UserName,
                TakenDate = DateTime.Now
            };

            await _activeBookReservationsRepo.InsertAsync(reservation);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(AssignBookToMemberCommand request, CancellationToken cancellationToken)
        {
            await CheckMemberAndBookAsync(request.Dto.UserName, request.Dto.BookId);
            await CheckAvailableBooks(request.Dto.BookId);
            //get first record on the basis of TakenDate
            var activeReservation = await _activeBookReservationsRepo.GetDataWithLinqExp(x => x.MemberId == request.Dto.UserName
                                                                                           && x.BookId == request.Dto.BookId)
                                                                     .OrderBy(x => x.TakenDate)
                                                                     .FirstOrDefaultAsync(cancellationToken);

            var bookRecord = await _booksRepo.GetByIdAsync(request.Dto.BookId);
            bookRecord.ReservedCount -= 1;
            bookRecord.AvailableCount += 1;

            if (activeReservation is null)
                throw new CustomBusinessException("Active book reservation could not be found.");

            var historyRecord = new BookReservationHistories()
            {
                BookId =  request.Dto.BookId,
                MemberId =  request.Dto.UserName,
                TakenDate = activeReservation.TakenDate,
                GivenDate = DateTime.Now
            };
            await _bookReservationHistoriesRepo.InsertAsync(historyRecord);
            _activeBookReservationsRepo.Delete(activeReservation);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }

        #region private methods
        private async Task<bool> CheckIfAnyAvailableBooksAsync(int bookId)
        {
            short availableBookCount = await _booksRepo.GetDataWithLinqExp(x => x.BookId == bookId)
                                                    .Select(x => x.AvailableCount)
                                                    .SingleOrDefaultAsync();

            return availableBookCount > 0;
        }

        private async Task<bool> CheckIfBookExistsAsync(int bookId)
        {
            return await _booksRepo.DoesEntityExistAsync(bookId);
        }

        private async Task<bool> CheckIfMemberExistsAsync(string UserName)
        {
            return await _membersRepo.DoesEntityExistAsync(UserName);
        }


        private async Task CheckMemberAndBookAsync(string userName, int bookId)
        {
            var memberExistTask = CheckIfMemberExistsAsync(userName);
            var bookExistTask = CheckIfBookExistsAsync(bookId);

            await Task.WhenAll(memberExistTask, bookExistTask); //parallel request to db.
            bool memberExist = memberExistTask.Result;
            bool bookExist = bookExistTask.Result;

            if (!memberExist)
                throw new CustomBusinessException("User name does not exist on system.");

            if (!bookExist)
                throw new CustomBusinessException("Book does not exist on system.");
            
        }

        private async Task CheckAvailableBooks(int bookId)
        {
            bool isThereAnyAvailableBook = await CheckIfAnyAvailableBooksAsync(bookId);

            if (isThereAnyAvailableBook is false)
            {
                throw new CustomBusinessException($"Sorry! This book is not available now.");
            }
        }
        #endregion
    }
}
