using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.Configurations
{
    //TODO: delete file
    internal class ActiveBookReservationsConfiguration //: IEntityTypeConfiguration<ActiveBookReservations>
    {
        public void Configure(EntityTypeBuilder<ActiveBookReservations> builder)
        {
            builder.HasKey(acr => acr.ReservationId);
            builder.HasOne(acr => acr.Book)
                .WithMany(b => b.ActiveBookReservations)
                .HasForeignKey(acr => acr.BookId);
            builder.HasOne(acr => acr.Member)
                .WithMany(m => m.ActiveBookReservations)
                .HasForeignKey(acr => acr.MemberId);

            builder.Ignore(acr => acr.AvailableAt);
        }
    }
}
