using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ReservationHistoryBookDto
    {
        [Key]
        [DisplayName("Book")]
        public int BookId { get; set; }
    }
}
