using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Queries.BookReservation;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Repositories;
using CityLibraryInfrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CityLibraryInfrastructure.ExceptionHandling;

namespace CityLibraryApi.Handlers.Queries.BookReservation
{
    public class BookReservationQueryHandlers : IRequestHandler<GetAllActiveBookReservationsQuery, IEnumerable<ActiveBookReservationsResponseDto>>,
                                                IRequestHandler<GetNumberOfBooksPerTitleAndEditionNumberQuery, IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>>,
                                                IRequestHandler<GetNumberOfBooksReservedPerMembersQuery, IEnumerable<NumberOfBooksReservedByMembersResponseDto>>,
                                                IRequestHandler<GetReservationHistoryByBookQuery, IEnumerable<ReservationHistoryBookResponseDto>>,
                                                IRequestHandler<GetReservationHistoryByMemberQuery, IEnumerable<ReservationHistoryMemberResponseDto>>,
                                                IRequestHandler<GetReservationHistoryPerBookQuery, IEnumerable<ReservationHistoryBookResponseDto>>,
                                                IRequestHandler<GetReservationHistoryPerMemberQuery, IEnumerable<ReservationHistoryMemberResponseDto>>,
                                                IRequestHandler<GetReservedBooksEstimatedReturnDatesQuery, IEnumerable<DateTime>>
    {
        private readonly IActiveBookReservationsRepo _activeBookReservationsRepo;
        private readonly IBookReservationHistoriesRepo _bookReservationHistoriesRepo;
        private readonly IMembersRepo _membersRepo;
        private readonly IBooksRepo _booksRepo;
        public BookReservationQueryHandlers(
            IActiveBookReservationsRepo activeBookReservationsRepo,
            IBookReservationHistoriesRepo bookReservationHistoriesRepo,
            IMembersRepo membersRepo,
            IBooksRepo booksRepo)
        {
            _activeBookReservationsRepo = activeBookReservationsRepo;
            _bookReservationHistoriesRepo = bookReservationHistoriesRepo;
            _membersRepo = membersRepo;
            _booksRepo = booksRepo;
        }

        public async Task<IEnumerable<ActiveBookReservationsResponseDto>> Handle(GetAllActiveBookReservationsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var baseData = _activeBookReservationsRepo.GetData();

            if (filter is not null)
            {
                if (!string.IsNullOrEmpty(filter.BookTitle))
                    baseData = baseData.Where(x => x.Book.BookTitle == filter.BookTitle);

                if (!string.IsNullOrEmpty(filter.UserName))
                    baseData = baseData.Where(x => x.MemberId == filter.UserName);
            }

            var result = await baseData.Select(x => new ActiveBookReservationsResponseDto
            {
                TakenDate = x.TakenDate,
                AvailableAt = x.AvailableAt,
                UserName = x.Member.UserName,
                MemberFullName = x.Member.FullName,
                BookTitle = x.Book.BookTitle,
                EditionNumber = x.Book.EditionNumber,
                CoverType = x.Book.CoverType,
                AvailableCount = x.Book.AvailableCount,
                ReservedCount = x.Book.ReservedCount
            }).ToArrayAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>> Handle(GetNumberOfBooksPerTitleAndEditionNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _booksRepo.GetData()
                                         .GroupBy(x => new { x.BookTitle, x.EditionNumber })
                                         .Select(x => new NumberOfBooksPerTitleAndEditionNumberResponseDto
                                         {
                                             BookTitle = x.Key.BookTitle,
                                             EditionNumber = x.Key.EditionNumber,
                                             Count = x.Sum(y => y.AvailableCount)
                                         }).ToArrayAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<NumberOfBooksReservedByMembersResponseDto>> Handle(GetNumberOfBooksReservedPerMembersQuery request, CancellationToken cancellationToken)
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
                                                          .ToArrayAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> Handle(GetReservationHistoryByBookQuery request, CancellationToken cancellationToken)
        {
            var result = await(from z in _bookReservationHistoriesRepo.GetData().Where(x => x.BookId == request.Dto.BookId)
                               select new { z.BookId, z.MemberId, z.TakenDate, z.GivenDate } into BookMember
                               let book = _booksRepo.GetById(BookMember.BookId)
                               let member = _membersRepo.GetById(BookMember.MemberId)
                               select new ReservationHistoryBookResponseDto
                               {
                                   BookTitle = book.BookTitle,
                                   FirstPublishDate = book.FirstPublishDate,
                                   EditionNumber = book.EditionNumber,
                                   EditionDate = book.EditionDate,
                                   TakenDate = BookMember.TakenDate,
                                   GivenDate = BookMember.GivenDate,
                                   UserName = BookMember.MemberId,
                                   FullName = member.FullName
                               }).ToArrayAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> Handle(GetReservationHistoryByMemberQuery request, CancellationToken cancellationToken)
        {
            var result = await(from z in _bookReservationHistoriesRepo.GetData().Where(x => x.MemberId == request.Dto.UserName)
                               select new { z.BookId, z.MemberId, z.TakenDate, z.GivenDate } into BookMember
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
                                   TakenDate = BookMember.TakenDate,
                                   GivenDate = BookMember.GivenDate
                               }).ToArrayAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> Handle(GetReservationHistoryPerBookQuery request, CancellationToken cancellationToken)
        {
            //TODO: farklı query düzenleri deneme
            var dto = request.Dto;
            var baseQuery = (from z in _bookReservationHistoriesRepo.GetData()
                             select new { z.BookId, z.MemberId, z.TakenDate, z.GivenDate } into BookMember
                             let book = _booksRepo.GetById(BookMember.BookId)
                             let member = _membersRepo.GetById(BookMember.MemberId)
                             select new ReservationHistoryBookResponseDto
                             {
                                 BookTitle = book.BookTitle,
                                 FirstPublishDate = book.FirstPublishDate,
                                 EditionNumber = book.EditionNumber,
                                 EditionDate = book.EditionDate,
                                 TakenDate = BookMember.TakenDate,
                                 GivenDate = BookMember.GivenDate,
                                 UserName = BookMember.MemberId,
                                 FullName = member.FullName
                             }).OrderBy(dto.SortingModel).Skip(dto.Skip);

            if (dto.Take > 0)
                baseQuery = baseQuery.Take(dto.Take);

            return await baseQuery.ToArrayAsync(cancellationToken);
        }

        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> Handle(GetReservationHistoryPerMemberQuery request, CancellationToken cancellationToken)
        {
            //TODO: farklı query düzenleri deneme
            var dto = request.Dto;
            var baseQuery = (from z in _bookReservationHistoriesRepo.GetData()
                             select new { z.BookId, z.MemberId, z.TakenDate, z.GivenDate } into BookMember
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
                                 TakenDate = BookMember.TakenDate,
                                 GivenDate = BookMember.GivenDate
                             }).OrderBy(dto.SortingModel).Skip(dto.Skip);

            if (dto.Take > 0)
                baseQuery = baseQuery.Take(dto.Take);


            return await baseQuery.ToArrayAsync(cancellationToken);
        }

        public async Task<IEnumerable<DateTime>> Handle(GetReservedBooksEstimatedReturnDatesQuery request, CancellationToken cancellationToken)
        {
            var result = _activeBookReservationsRepo.GetDataWithLinqExp(x => x.BookId == request.Dto.BookId)
                                                    .AsEnumerable()
                                                    .Select(x => x.AvailableAt)
                                                    .OrderBy(x => x);

            if (!result.Any())
                throw new CustomBusinessException("There is no resevartion of the searched book.");

            return await Task.FromResult(result);
        }
    }
}
