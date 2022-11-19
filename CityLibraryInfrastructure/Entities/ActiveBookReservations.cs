using CityLibraryInfrastructure.DbBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Entities
{
    public class ActiveBookReservations : TableBase
    {
        public int ReservationId { get; set; }

        public DateTime ReturnDate { get; set; }

        //Default return period 7 days
        public DateTime AvailableAt => ReturnDate.AddDays(7);

        public string MemberId { get; set; }

        public Members Member { get; set; }

        public int BookId { get; set; }

        public Books Book { get; set; }
    }
}
