using CityLibraryInfrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ActiveBookReservationsResponseDto
    {
        public DateTime ReturnDate { get; set; }

        public DateTime AvailableAt { get; set; }

        public string UserName { get; set; }

        public string MemberFullName { get; set; }

        public string BookTitle { get; set; }

        public short EditionNumber { get; set; }

        public BookCoverTypes CoverType { get; set; }

        public short AvailableCount { get; set; }

        public short ReservedCount { get; set; }
    }
}
