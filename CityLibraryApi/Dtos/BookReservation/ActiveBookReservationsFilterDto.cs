using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ActiveBookReservationsFilterDto
    {
        public string UserName { get; set; }

        public string BookTitle { get; set; }
    }
}
