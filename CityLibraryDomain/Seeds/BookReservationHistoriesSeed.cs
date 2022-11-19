using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Seeds
{
    class BookReservationHistoriesSeed : IEntityTypeConfiguration<BookReservationHistories>
    {
        public void Configure(EntityTypeBuilder<BookReservationHistories> builder)
        {
            builder.HasData(
                 new BookReservationHistories
                 {
                     HistoryId = 1,
                     BookId = 1,
                     MemberId = "User3",
                     ReturnDate = DateTime.Now.AddDays(-40),
                     RecievedDate = DateTime.Now.AddDays(-20)
                 },
                 new BookReservationHistories
                 {
                     HistoryId = 2,
                     BookId = 2,
                     MemberId = "User3",
                     ReturnDate = DateTime.Now.AddDays(-12),
                     RecievedDate = DateTime.Now.AddDays(-3)
                 },
                 new BookReservationHistories
                 {
                     HistoryId = 3,
                     BookId = 5,
                     MemberId = "User1",
                     ReturnDate = DateTime.Now.AddDays(-22),
                     RecievedDate = DateTime.Now.AddDays(-13)
                 },
                 new BookReservationHistories
                 {
                     HistoryId = 4,
                     BookId = 5,
                     MemberId = "User2",
                     ReturnDate = DateTime.Now.AddDays(-120),
                     RecievedDate = DateTime.Now.AddDays(-100)
                 }
            );
        }
    }
}
