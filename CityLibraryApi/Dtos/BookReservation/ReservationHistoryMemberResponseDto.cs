using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ReservationHistoryMemberResponseDto
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string BookTitle { get; set; }

        public DateTime FirstPublishDate { get; set; }

        public short EditionNumber { get; set; }

        public DateTime EditionDate { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime GivenDate { get; set; }

        //public ReservationHistoryInnerBookInfo BookInfo { get; set; }
    }

    /*
    public class ReservationHistoryInnerBookInfo
    {
        public string BookTitle { get; set; }

        public DateTime FirstPublishDate { get; set; }

        public short EditionNumber { get; set; }

        public DateTime EditionDate { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime GivenDate { get; set; }
    }
    */
}
