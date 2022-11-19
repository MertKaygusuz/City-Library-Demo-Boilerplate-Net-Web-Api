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
    internal class MembersConfiguration : IEntityTypeConfiguration<Members>
    {
        public void Configure(EntityTypeBuilder<Members> builder)
        {
            builder.HasKey(m => m.UserName);
            builder.Property(m => m.UserName)
                .HasMaxLength(30);

            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.BirthDate)
                .IsRequired();
            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(m => m.Password)
                .IsRequired();

            #region many-to-many configurations
            builder.HasMany(m => m.Roles)
                .WithMany(r => r.Members)
                .UsingEntity<MemberRoles>(
                    x => x.HasOne(mr => mr.Role).WithMany(r => r.MemberRoles).HasForeignKey(mr => mr.RoleId),
                    x => x.HasOne(mr => mr.Member).WithMany(m => m.MemberRoles).HasForeignKey(mr => mr.MemberId),
                    x => x.HasKey(mr => mr.Id)
                );


            builder.HasMany(m => m.PreviouslyRecievedBooks)
                .WithMany(b => b.MemberInfoForPreviousReservations)
                .UsingEntity<BookReservationHistories>(
                    x => x.HasOne(brh => brh.Book).WithMany(b => b.BookReservationHistories).HasForeignKey(brh => brh.BookId),
                    x => x.HasOne(brh => brh.Member).WithMany(m => m.BookReservationHistories).HasForeignKey(brh => brh.MemberId),
                    x => x.HasKey(brh => brh.HistoryId)
                );

            builder.HasMany(m => m.RecievedBooks)
                .WithMany(b => b.MemberInfoForActiveReservations)
                .UsingEntity<ActiveBookReservations>(
                    x => x.HasOne(abr => abr.Book).WithMany(b => b.ActiveBookReservations).HasForeignKey(abr => abr.BookId),
                    x => x.HasOne(abr => abr.Member).WithMany(m => m.ActiveBookReservations).HasForeignKey(abr => abr.MemberId),
                    x => 
                    {
                        x.HasKey(abr => abr.ReservationId);
                        x.Ignore(abr => abr.AvailableAt);
                    }
                );
            #endregion
        }
    }
}
