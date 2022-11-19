using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Dtos.BookReservation
{
    public class ReservationHistoryBookResponseDto
    {
        public string BookTitle { get; set; }

        public DateTime FirstPublishDate { get; set; }

        public short EditionNumber { get; set; }

        public DateTime EditionDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime RecievedDate { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        //public ReservationHistoryInnerMemberInfo MemberInfo { get; set; }
    }

    /*
    public class ReservationHistoryInnerMemberInfo
    {
        public string UserName { get; set; }

        public string FullName { get; set; }
    }
    */
}
