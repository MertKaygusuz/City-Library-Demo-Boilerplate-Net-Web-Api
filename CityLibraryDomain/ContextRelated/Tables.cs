using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.ContextRelated
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Members> Members { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<ActiveBookReservations> ActiveBookReservations { get; set; }

        public DbSet<BookReservationHistories> BookReservationHistory { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<MemberRoles> MemberRoles { get; set; }
    }
}
