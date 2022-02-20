using CityLibraryInfrastructure.DbBase;
using CityLibraryInfrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Entities
{
    public class Books : TableBase
    {
        public int BookId { get; set; }

        public string Author { get; set; }

        public string BookTitle { get; set; }

        public DateTime FirstPublishDate { get; set; }

        public short EditionNumber { get; set; }

        public DateTime EditionDate { get; set; }

        public BookTitleTypes TitleType { get; set; }

        public BookCoverTypes CoverType  { get; set; }

        public short AvailableCount { get; set; }

        public short ReservedCount { get; set; }

        //virtual, might be lazy loaded
        public virtual ICollection<BookReservationHistories> BookReservationHistories { get; set; }

        public virtual List<Members> MemberInfoForPreviousReservations { get; set; }

        public virtual ICollection<ActiveBookReservations> ActiveBookReservations { get; set; }

        public virtual List<Members> MemberInfoForActiveReservations { get; set; }
    }
}
