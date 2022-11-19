using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Services.BookReservation.Interfaces;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.BaseInterfaces.Pagination;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.Extensions;
using CityLibraryInfrastructure.MapperConfigurations;
using CityLibraryInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryApi.Services.BookReservation.Classes
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ExceptionsResource> _localizer;
        private readonly IActiveBookReservationsRepo _activeBookReservationsRepo;
        private readonly IBookReservationHistoriesRepo _bookReservationHistoriesRepo;
        private readonly IMembersRepo _membersRepo;
        private readonly IBooksRepo _booksRepo;
        public BookReservationService(
            IUnitOfWork unitOfWork,
            IStringLocalizer<ExceptionsResource> localizer,
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
            _localizer = localizer;
        }

        public async Task AssignBookToMemberAsync(AssignBookToMemberDto dto)
        {
            var theBook = await _booksRepo.GetByIdAsync(dto.BookId);
            theBook.AvailableCount -= 1;
            theBook.ReservedCount += 1;
            ActiveBookReservations reservation = new()
            {
                BookId = dto.BookId,
                MemberId = dto.UserName,
                ReturnDate = DateTime.Now
            };

            await _activeBookReservationsRepo.InsertAsync(reservation);

            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CheckIfAnyAvailableBooksAsync(int bookId)
        {
            short availableBookCount = await _booksRepo.GetDataWithLinqExp(x => x.BookId == bookId)
                                                    .Select(x => x.AvailableCount)
                                                    .SingleOrDefaultAsync();

            return availableBookCount > 0;
        }

        public async Task<bool> CheckIfBookExistsAsync(int bookId)
        {
            return await _booksRepo.DoesEntityExistAsync(bookId);
        }

        public async Task<bool> CheckIfMemberExistsAsync(string UserName)
        {
            return await _membersRepo.DoesEntityExistAsync(UserName);
        }

        public async Task<IEnumerable<ActiveBookReservationsResponseDto>> GetAllActiveBookReservationsAsync(ActiveBookReservationsFilterDto filter)
        {
            var baseData = _activeBookReservationsRepo.GetData();
            
            if(filter is not null)
            {
                if (!string.IsNullOrEmpty(filter.BookTitle))
                    baseData = baseData.Where(x => x.Book.BookTitle == filter.BookTitle);

                if (!string.IsNullOrEmpty(filter.UserName))
                    baseData = baseData.Where(x => x.MemberId == filter.UserName);
            }
            
            var result = await baseData.Select(x => new ActiveBookReservationsResponseDto
            {
                ReturnDate = x.ReturnDate,
                AvailableAt = x.AvailableAt,
                UserName = x.Member.UserName,
                MemberFullName = x.Member.FullName,
                BookTitle = x.Book.BookTitle,
                EditionNumber = x.Book.EditionNumber,
                CoverType = x.Book.CoverType,
                AvailableCount = x.Book.AvailableCount,
                ReservedCount = x.Book.ReservedCount
            }).ToArrayAsync();
            return result;
        }

        public async Task<IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>> GetNumberOfBooksPerTitleAndEditionNumberAsync()
        {
            var result = await _booksRepo.GetData()
                                         .GroupBy(x => new { x.BookTitle, x.EditionNumber })
                                         .Select(x => new NumberOfBooksPerTitleAndEditionNumberResponseDto
                                         {
                                             BookTitle = x.Key.BookTitle,
                                             EditionNumber = x.Key.EditionNumber,
                                             Count = x.Sum(y => y.AvailableCount)
                                         }).ToArrayAsync();

            return result;
        }

        public async Task<IEnumerable<NumberOfBooksReservedByMembersResponseDto>> GetNumberOfBooksReservedPerMembersAsync()
        {
            //TODO: groupby incele
            var result = await _activeBookReservationsRepo.GetData()
                                                          .Select(x => new
                                                          {
                                                              x.MemberId,
                                                              x.Member.FullName
                                                          })
                                                          .GroupBy(x => new { x.MemberId, x.FullName })
                                                          .Select(x => new NumberOfBooksReservedByMembersResponseDto
                                                          {
                                                              MemberName = x.Key.MemberId,
                                                              MemberFullName = x.Key.FullName,
                                                              ActiveBookReservationsCount = x.Count()
                                                          })
                                                          .ToArrayAsync();


            return result;
        }

        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryByBookAsync(ReservationHistoryBookDto dto)
        {
            var result = await (from z in _bookReservationHistoriesRepo.GetData().Where(x => x.BookId == dto.BookId)
                                select new { z.BookId, z.MemberId, z.ReturnDate, z.RecievedDate } into BookMember
                                let book = _booksRepo.GetById(BookMember.BookId)
                                let member = _membersRepo.GetById(BookMember.MemberId)
                                select new ReservationHistoryBookResponseDto
                                {
                                    BookTitle = book.BookTitle,
                                    FirstPublishDate = book.FirstPublishDate,
                                    EditionNumber = book.EditionNumber,
                                    EditionDate = book.EditionDate,
                                    ReturnDate = BookMember.ReturnDate,
                                    RecievedDate = BookMember.RecievedDate,
                                    UserName = BookMember.MemberId,
                                    FullName = member.FullName
                                }).ToArrayAsync();

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryByMemberAsync(ReservationHistoryMemberDto dto)
        {
            var result = await (from z in _bookReservationHistoriesRepo.GetData().Where(x => x.MemberId == dto.UserName)
                                select new { z.BookId, z.MemberId, z.ReturnDate, z.RecievedDate } into BookMember
                                let book = _booksRepo.GetById(BookMember.BookId)
                                let member = _membersRepo.GetById(BookMember.MemberId)
                                select new ReservationHistoryMemberResponseDto
                                {
                                    UserName = BookMember.MemberId,
                                    FullName = member.FullName,
                                    BookTitle = book.BookTitle,
                                    FirstPublishDate = book.FirstPublishDate,
                                    EditionNumber = book.EditionNumber,
                                    EditionDate = book.EditionDate,
                                    ReturnDate = BookMember.ReturnDate,
                                    RecievedDate = BookMember.RecievedDate
                                }).ToArrayAsync();

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryPerBookAsync(ReservationHistoryPerBookDto dto)
        {
            //TODO: farklı query düzenleri deneme
            var baseQuery = (from z in _bookReservationHistoriesRepo.GetData()
                            select new { z.BookId, z.MemberId, z.ReturnDate, z.RecievedDate } into BookMember
                            let book = _booksRepo.GetById(BookMember.BookId)
                            let member = _membersRepo.GetById(BookMember.MemberId)
                            select new ReservationHistoryBookResponseDto
                            {
                                BookTitle = book.BookTitle,
                                FirstPublishDate = book.FirstPublishDate,
                                EditionNumber = book.EditionNumber,
                                EditionDate = book.EditionDate,
                                ReturnDate = BookMember.ReturnDate,
                                RecievedDate = BookMember.RecievedDate,
                                UserName = BookMember.MemberId,
                                FullName = member.FullName
                            }).OrderBy(dto.SortingModel).Skip(dto.Skip);

            if (dto.Take > 0)
                baseQuery = baseQuery.Take(dto.Take);

            return await baseQuery.ToArrayAsync();
        }

        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryPerMemberAsync(ReservationHistoryPerMemberDto dto)
        {
            //TODO: farklı query düzenleri deneme
            var baseQuery = (from z in _bookReservationHistoriesRepo.GetData()
                             select new { z.BookId, z.MemberId, z.ReturnDate, z.RecievedDate } into BookMember
                             let book = _booksRepo.GetById(BookMember.BookId)
                             let member = _membersRepo.GetById(BookMember.MemberId)
                             select new ReservationHistoryMemberResponseDto
                             {
                                 UserName = BookMember.MemberId,
                                 FullName = member.FullName,
                                 BookTitle = book.BookTitle,
                                 FirstPublishDate = book.FirstPublishDate,
                                 EditionNumber = book.EditionNumber,
                                 EditionDate = book.EditionDate,
                                 ReturnDate = BookMember.ReturnDate,
                                 RecievedDate = BookMember.RecievedDate
                             }).OrderBy(dto.SortingModel).Skip(dto.Skip);

            if (dto.Take > 0)
                baseQuery = baseQuery.Take(dto.Take);
            

            return await baseQuery.ToArrayAsync();
        }

        public IEnumerable<DateTime> GetReservedBooksEstimatedReturnDates(ReservedBookEstimatedReturnDatesDto dto)
        {
            var result = _activeBookReservationsRepo.GetDataWithLinqExp(x => x.BookId == dto.BookId)
                                                                                .AsEnumerable()
                                                                                .Select(x => x.AvailableAt)
                                                                                .Order();
                                                          
            if (!result.Any())
                throw new CustomBusinessException(_localizer["No_Reservation_On_Search"]);

            return result;
        }

        public async Task UnAssignBookFromUserAsync(AssignBookToMemberDto dto)
        {
            //get first record on the basis of ReturnDate
            var activeReservation = await _activeBookReservationsRepo.GetDataWithLinqExp(x => x.MemberId == dto.UserName
                                                                                           && x.BookId == dto.BookId)
                                                                     .OrderBy(x => x.ReturnDate)
                                                                     .FirstOrDefaultAsync();

            var bookRecord = await _booksRepo.GetByIdAsync(dto.BookId);
            bookRecord.ReservedCount -= 1;
            bookRecord.AvailableCount += 1;

            if (activeReservation is null)
                throw new CustomBusinessException(_localizer["Active_Book_Reservation_Not_Found"]);

            var historyRecord = new BookReservationHistories()
            {
                BookId = dto.BookId,
                MemberId = dto.UserName,
                ReturnDate = activeReservation.ReturnDate,
                RecievedDate = DateTime.Now
            };
            await _bookReservationHistoriesRepo.InsertAsync(historyRecord);
            _activeBookReservationsRepo.Delete(activeReservation);
            await _unitOfWork.CommitAsync();
        }
    }
}
