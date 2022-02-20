using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ReservationHistoryMemberDto
    {
        [Key]
        [DisplayName("Member")]
        public string UserName { get; set; }
    }
}
