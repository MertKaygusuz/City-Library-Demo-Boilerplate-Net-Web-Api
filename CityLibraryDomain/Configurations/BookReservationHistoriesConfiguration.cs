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
    internal class BookReservationHistoriesConfiguration //: IEntityTypeConfiguration<BookReservationHistories>
    {
        public void Configure(EntityTypeBuilder<BookReservationHistories> builder)
        {
            builder.HasKey(brh => brh.HistoryId);
            builder.HasOne(brh => brh.Book)
                .WithMany(b => b.BookReservationHistories)
                .HasForeignKey(brh => brh.BookId);
            builder.HasOne(brh => brh.Member)
                .WithMany(m => m.BookReservationHistories)
                .HasForeignKey(brh => brh.MemberId);
        }
    }
}
