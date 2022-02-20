using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class NumberOfBooksPerTitleAndEditionNumberResponseDto
    {
        public string BookTitle { get; set; }

        public short EditionNumber { get; set; }

        public int Count { get; set; }
    }
}
