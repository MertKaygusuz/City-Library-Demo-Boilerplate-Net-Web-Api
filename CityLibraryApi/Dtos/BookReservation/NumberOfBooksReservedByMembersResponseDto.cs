using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class NumberOfBooksReservedByMembersResponseDto
    {
        public string MemberName { get; set; }

        public string MemberFullName { get; set; }

        public int ActiveBookReservationsCount { get; set; }
    }
}
