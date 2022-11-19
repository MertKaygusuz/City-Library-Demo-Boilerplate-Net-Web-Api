using CityLibraryInfrastructure.DbBase;
using System;
using System.Collections.Generic;

namespace CityLibraryInfrastructure.Entities
{
    public class Members : TableBase
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        //virtual, might be lazy loaded
        public virtual ICollection<BookReservationHistories> BookReservationHistories { get; set; }

        public virtual List<Books> PreviouslyRecievedBooks { get; set; }

        public virtual ICollection<ActiveBookReservations> ActiveBookReservations { get; set; }

        public virtual List<Books> RecievedBooks { get; set; }

        public virtual ICollection<MemberRoles> MemberRoles { get; set; }

        public virtual List<Roles> Roles { get; set; }
    }
}
