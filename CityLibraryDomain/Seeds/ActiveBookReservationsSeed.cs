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
    class ActiveBookReservationsSeed : IEntityTypeConfiguration<ActiveBookReservations>
    {
        public void Configure(EntityTypeBuilder<ActiveBookReservations> builder)
        {
            builder.HasData(
                 new ActiveBookReservations
                 {
                     ReservationId = 1,
                     BookId = 1,
                     MemberId = "User2",
                     ReturnDate = DateTime.Now.AddDays(-4)
                 },
                 new ActiveBookReservations
                 {
                     ReservationId = 2,
                     BookId = 2,
                     MemberId = "User2",
                     ReturnDate = DateTime.Now.AddDays(-2)
                 },
                 new ActiveBookReservations
                 {
                     ReservationId = 3,
                     BookId = 2,
                     MemberId = "User1",
                     ReturnDate = DateTime.Now.AddDays(-6)
                 }
            );
        }
    }
}
