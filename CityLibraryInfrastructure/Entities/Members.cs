using CityLibraryInfrastructure.DbBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual List<Books> PreviousTakenBooks { get; set; } //TODO: naming

        public virtual ICollection<ActiveBookReservations> ActiveBookReservations { get; set; }

        public virtual List<Books> TakenBooks { get; set; } //TODO: naming

        public virtual ICollection<MemberRoles> MemberRoles { get; set; }

        public virtual List<Roles> Roles { get; set; }
    }
}
